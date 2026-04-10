using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";      // PDF to which the portfolio will be attached
        const string portfolioPdfPath  = "portfolio.pdf";  // PDF portfolio file to embed
        const string outputPdfPath     = "output.pdf";

        if (!File.Exists(inputPdfPath) || !File.Exists(portfolioPdfPath))
        {
            Console.Error.WriteLine("Input PDF or portfolio PDF not found.");
            return;
        }

        // Load the target PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // 1. Embed the portfolio PDF as an attached file in the document.
            // -----------------------------------------------------------------
            // Create a file specification for the portfolio file.
            FileSpecification portfolioSpec = new FileSpecification(portfolioPdfPath);
            // Add the specification to the document's embedded files collection.
            doc.EmbeddedFiles.Add(portfolioSpec);

            // -----------------------------------------------------------------
            // 2. Create a visible file attachment annotation on the first page.
            // -----------------------------------------------------------------
            Page firstPage = doc.Pages[1];
            // Fully qualified rectangle to avoid ambiguity with System.Drawing.
            Aspose.Pdf.Rectangle annotRect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);
            // Construct the annotation using the page, rectangle, and file spec.
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(firstPage, annotRect, portfolioSpec)
            {
                // Optional visual and descriptive properties.
                Icon     = FileIcon.PushPin, // Fixed: use FileIcon enum instead of non‑existent FileAttachmentAnnotationIcons
                Contents = "Embedded PDF portfolio",
                Subject  = "Portfolio Document"
            };
            // Add the annotation to the page.
            firstPage.Annotations.Add(attachment);

            // -----------------------------------------------------------------
            // 3. Set custom description metadata for the PDF.
            // -----------------------------------------------------------------
            // Using standard document info (Subject) as a description.
            doc.Info.Subject = "Custom description for the PDF document";

            // Adding a custom XMP metadata entry named "Description".
            doc.Metadata.Add("Description", "Custom description for the PDF document");

            // -----------------------------------------------------------------
            // 4. Save the modified PDF.
            // -----------------------------------------------------------------
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with attached portfolio saved to '{outputPdfPath}'.");
    }
}
