using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_attachment.pdf";
        const string attachmentPath = "sample.txt";
        const string unicodeFileName = "文件附件.txt"; // Unicode filename to display in PDF viewers

        // Ensure a source PDF exists; create a simple one if missing.
        if (!File.Exists(inputPath))
        {
            using (Document doc = new Document())
            {
                doc.Pages.Add(); // add a blank page
                doc.Save(inputPath);
            }
        }

        // Ensure the file to be attached exists.
        if (!File.Exists(attachmentPath))
        {
            File.WriteAllText(attachmentPath, "Sample attachment content.");
        }

        // Load the PDF, add a file attachment annotation with a Unicode name, and save.
        using (Document doc = new Document(inputPath))
        {
            // Create a FileSpecification for the attachment.
            FileSpecification fileSpec = new FileSpecification(attachmentPath);
            // Set the Unicode name that PDF viewers will show.
            fileSpec.UnicodeName = unicodeFileName;

            // Define the rectangle where the attachment icon will appear.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create the attachment annotation on the first page.
            Page page = doc.Pages[1];
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Optional visual settings.
                Icon = FileIcon.Paperclip, // Correct enum for attachment icons
                Contents = "Embedded file with Unicode name",
                Color = Aspose.Pdf.Color.Blue
            };

            // Add the annotation to the page.
            page.Annotations.Add(attachment);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Attachment added with Unicode filename. Saved to '{outputPath}'.");
    }
}
