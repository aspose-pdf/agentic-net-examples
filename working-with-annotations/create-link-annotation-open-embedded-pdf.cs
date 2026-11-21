using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";      // PDF to modify
        const string attachmentPdf = "attachment.pdf"; // PDF to embed
        const string outputPdf = "output.pdf";   // Resulting file

        if (!File.Exists(inputPdf) || !File.Exists(attachmentPdf))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // ------------------------------------------------------------
            // Embed the PDF attachment into the document
            // ------------------------------------------------------------
            // Create a FileSpecification for the attachment and add it to the EmbeddedFiles collection.
            FileSpecification fileSpec = new FileSpecification(attachmentPdf, "Embedded PDF attachment");
            // Optional: set modification date metadata
            fileSpec.Params.ModDate = DateTime.UtcNow;
            doc.EmbeddedFiles.Add(fileSpec);

            // ------------------------------------------------------------
            // Create a link annotation on the first page that opens the attachment
            // ------------------------------------------------------------
            Page page = doc.Pages[1]; // 1‑based indexing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);
            LinkAnnotation link = new LinkAnnotation(page, rect);

            // Configure a LaunchAction to open the embedded PDF via its file name.
            // The LaunchAction constructor that accepts a string (file name) is used because the
            // version of Aspose.Pdf in the project does not provide an overload that takes a FileSpecification.
            LaunchAction launch = new LaunchAction(attachmentPdf);
            link.Action = launch;

            // Optional visual styling
            link.Color = Aspose.Pdf.Color.Blue;
            link.Border = new Border(link) { Width = 1 };

            // Add the annotation to the page
            page.Annotations.Add(link);

            // ------------------------------------------------------------
            // Save the modified document
            // ------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Document saved to '{outputPdf}'.");
    }
}
