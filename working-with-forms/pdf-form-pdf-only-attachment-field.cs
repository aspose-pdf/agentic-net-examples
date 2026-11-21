using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "form_with_attachment.pdf";
        const string dummyAttachmentPath = "dummy.pdf"; // placeholder file for the annotation

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a dummy PDF file so the annotation has a valid file reference.
        // In a real scenario the user would replace this with the actual PDF they want to attach.
        if (!File.Exists(dummyAttachmentPath))
        {
            using (var doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(dummyAttachmentPath);
            }
        }

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to host the attachment annotation
            Page page = doc.Pages.Add();

            // Define the rectangle where the attachment icon will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 120, 620);

            // Create the file attachment annotation. Aspose.Pdf does not provide a form field that
            // restricts the file type, so the restriction to PDF files must be enforced by the
            // consuming application (e.g., via JavaScript or a custom UI). Here we simply attach a
            // placeholder PDF file.
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(
                page,
                rect,
                new FileSpecification(dummyAttachmentPath, "Sample PDF Attachment")
            );

            // Set an icon for better visual appearance
            attachment.Icon = FileIcon.PushPin;

            // Configure a border for the annotation (Border requires the owning annotation in the ctor)
            attachment.Border = new Border(attachment)
            {
                Width = 1,
                Style = BorderStyle.Solid
            };
            attachment.Color = Color.LightGray;

            // Add the annotation to the page
            page.Annotations.Add(attachment);

            // Save the PDF document (Document.Save writes PDF regardless of extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with attachment field saved to '{outputPath}'.");
    }
}
