using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // PDF to which the annotation will be added
        const string outputPdfPath  = "output.pdf";         // Resulting PDF
        const string attachmentPath = "attachment.txt";     // File to be attached and opened on click

        // Verify that all required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Define the clickable area for the annotation (coordinates in points)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a FileSpecification for the attachment
            FileSpecification fileSpec = new FileSpecification(attachmentPath, Path.GetFileName(attachmentPath));

            // Create a file‑attachment annotation on the first page
            Page page = pdfDoc.Pages[1];
            FileAttachmentAnnotation fileAnn = new FileAttachmentAnnotation(page, linkRect, fileSpec)
            {
                Name = Path.GetFileName(attachmentPath), // Optional display name
                Contents = "Attached file"               // Tooltip text (optional)
            };

            // Add the annotation to the page
            page.Annotations.Add(fileAnn);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with file‑attachment annotation saved to '{outputPdfPath}'.");
    }
}
