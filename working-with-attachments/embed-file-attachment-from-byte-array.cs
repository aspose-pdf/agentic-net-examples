using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Sample byte array representing the file content to embed
        byte[] fileData = System.Text.Encoding.UTF8.GetBytes("Hello, this is the embedded file content.");

        // Path to the output PDF
        const string outputPdf = "output_with_attachment.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle for the attachment annotation (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a FileSpecification from the byte array using a MemoryStream
            using (MemoryStream ms = new MemoryStream(fileData))
            {
                // The second argument is the name that will appear in the attachment list
                FileSpecification fileSpec = new FileSpecification(ms, "EmbeddedFile.txt");

                // Create the file attachment annotation with the page, rectangle, and file spec
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
                {
                    // Optional: set a tooltip and icon for the annotation
                    Contents = "Embedded text file",
                    // Correct enum usage for the icon (FileIcon enum, not a nested type)
                    Icon = FileIcon.Paperclip,
                    Title = "Sample Attachment"
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(attachment);
            }

            // Save the PDF document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with embedded file saved to '{outputPdf}'.");
    }
}
