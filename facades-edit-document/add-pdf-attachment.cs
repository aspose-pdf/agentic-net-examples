using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string attachmentPath = "invoice.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Use the first page for the attachment annotation
            Page page = doc.Pages[1];

            // Define the annotation rectangle using Aspose.Pdf.Rectangle (LLX, LLY, URX, URY)
            float llx = 100f;
            float lly = 500f;
            float width = 200f;
            float height = 100f;
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, llx + width, lly + height);

            // Create a FileSpecification with description and (optional) MIME type
            // The constructor sets the file path and description. MIME type can be set if the property exists.
            FileSpecification fileSpec = new FileSpecification(attachmentPath, "Invoice Document");
            // If the MimeType property is available in the referenced version, set it; otherwise the default is used.
            // fileSpec.MimeType = "application/pdf"; // Uncomment if supported by the library version.

            // Create the file attachment annotation
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec);

            // Add the annotation to the page
            page.Annotations.Add(attachment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Attachment added and saved to '{outputPath}'.");
    }
}
