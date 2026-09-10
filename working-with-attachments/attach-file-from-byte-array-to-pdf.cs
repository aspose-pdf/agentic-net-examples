using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the source and destination PDFs
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_attachment.pdf";

        // Example byte array to be attached (could be any binary data)
        byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes("Hello, this is the attached file content.");
        const string attachmentFileName = "hello.txt";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPath}");
            return;
        }

        // Load the existing PDF document (lifecycle rule: load)
        using (Document doc = new Document(inputPath))
        {
            // Select the page where the attachment annotation will be placed (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle that bounds the annotation icon
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create a FileSpecification from the byte array using a MemoryStream
            using (MemoryStream ms = new MemoryStream(fileBytes))
            {
                FileSpecification fileSpec = new FileSpecification(ms, attachmentFileName);

                // Create the FileAttachmentAnnotation with the page, rectangle, and file spec
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
                {
                    // Optional visual and descriptive settings
                    Icon     = FileIcon.PushPin, // corrected enum
                    Title    = "File Attachment",
                    Contents = $"Attached file: {attachmentFileName}"
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(attachment);
            }

            // Save the modified PDF (lifecycle rule: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with attached file: {outputPath}");
    }
}
