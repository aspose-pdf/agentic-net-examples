using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output_with_attachment.pdf";
        const string attachmentPath = "attachment.bin"; // file to embed

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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Choose the page where the attachment annotation will be placed (first page)
            Page page = doc.Pages[1]; // 1‑based indexing

            // Define the rectangle for the annotation (coordinates in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 150, 750);

            // Create a FileSpecification for the external file
            FileSpecification fileSpec = new FileSpecification(attachmentPath);
            // Optional: set modification date or other parameters
            fileSpec.Params.ModDate = DateTime.UtcNow;

            // Create the file attachment annotation, passing the FileSpecification
            FileAttachmentAnnotation fileAnnot = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // "Contents" is the tooltip/description shown by PDF viewers
                Contents = "This is a very long description for the attached file. " +
                           "It may contain detailed information, usage instructions, " +
                           "metadata, or any other text that the end‑user might need to " +
                           "understand the purpose of the attachment. " +
                           "The description is stored within the PDF and will be displayed " +
                           "by PDF viewers that support file attachment annotations.",
                // Optional title shown in the attachment panel
                Title = "Embedded Attachment"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(fileAnnot);

            // Save the modified PDF; the attachment is preserved automatically
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachment: {outputPdfPath}");
    }
}
