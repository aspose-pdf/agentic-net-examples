using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "secured.pdf";
        const string password = "user123";
        const string attachmentPath = "attachment.txt";
        const string outputPath = "output.pdf";

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

        try
        {
            // Open the secured PDF with the user password
            using (Document doc = new Document(inputPath, password))
            {
                // Get the first page (or any page you want to attach the file to)
                Page page = doc.Pages[1];

                // Define the rectangle (left, bottom, right, top) for the annotation
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

                // Create a FileSpecification for the attachment file
                FileSpecification fileSpec = new FileSpecification(attachmentPath);

                // Create the FileAttachmentAnnotation with the required FileSpecification argument
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
                {
                    Name = Path.GetFileName(attachmentPath)
                };

                // Add the annotation to the page
                page.Annotations.Add(attachment);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"File attachment added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
