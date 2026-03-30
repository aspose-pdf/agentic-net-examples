using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string attachmentPath = "invoice.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Create a file specification for the attachment using the (filePath, description) constructor.
            var fileSpec = new FileSpecification(attachmentPath, "Invoice Document");
            // The MimeType property is not available in the current Aspose.Pdf version, so we omit it.

            // Define the annotation rectangle on the first page (coordinates are in points).
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100f, 500f, 200f, 600f);
            Page page = doc.Pages[1];

            // Create the file attachment annotation and add it to the page.
            FileAttachmentAnnotation fileAnn = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                Title = "Invoice Document",
                Contents = "Attached invoice PDF."
            };
            page.Annotations.Add(fileAnn);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Attachment added and saved to '{outputPath}'.");
    }
}
