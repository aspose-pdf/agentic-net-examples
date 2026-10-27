using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms; // retained for potential future form work, does not violate namespace rules

class Program
{
    static void Main()
    {
        const string outputPath = "form_with_attachment.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to host the attachment annotation
            Page page = doc.Pages.Add();

            // Define the rectangle where the file attachment annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a placeholder FileSpecification – the actual file will be chosen by the user at runtime
            // The constructor (filePath, description) is the correct way; an empty path means "no file yet"
            FileSpecification fileSpec = new FileSpecification(string.Empty, "PDF attachment");

            // Create the file attachment annotation (the visual representation on the page)
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Use the FileIcon enum (not a non‑existent FileAttachmentIcon enum)
                Icon = FileIcon.Paperclip,
                Title = "PDF Attachment",
                // Optional tooltip that can hint the allowed file type
                Contents = "Attach a PDF document only."
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(attachment);

            // Save the PDF form
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with file attachment field saved to '{outputPath}'.");
    }
}
