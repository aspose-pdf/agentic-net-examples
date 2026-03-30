using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string attachmentPath = "attachment.pdf";
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
            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Create a file specification using the constructor (path, description)
            FileSpecification fileSpec = new FileSpecification(attachmentPath, "Sample attachment");
            // Set the relationship type to Data (use the AFRelationship enum, not a string)
            fileSpec.AFRelationship = AFRelationship.Data;

            // Define the annotation rectangle using Aspose.Pdf.Rectangle (LLX, LLY, URX, URY)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 120, 120);

            // Create the file attachment annotation
            FileAttachmentAnnotation attachmentAnnotation = new FileAttachmentAnnotation(page, rect, fileSpec);
            attachmentAnnotation.Contents = "Attached file";

            // Add the annotation to the page
            page.Annotations.Add(attachmentAnnotation);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Attachment added and saved to '{outputPath}'.");
    }
}
