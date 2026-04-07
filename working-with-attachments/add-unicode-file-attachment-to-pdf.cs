using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Output PDF file
        const string outputPdf = "output.pdf";

        // File to be attached (ensure it exists)
        const string attachmentFile = "sample.txt";
        File.WriteAllText(attachmentFile, "Sample attachment content.");

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (required for the annotation)
            Page page = doc.Pages.Add();

            // Create a file specification for the attachment
            FileSpecification fileSpec = new FileSpecification(attachmentFile)
            {
                // Unicode filename that will be shown in PDF viewers
                UnicodeName = "示例文件.txt"
            };

            // Define the rectangle where the attachment icon will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create the file attachment annotation on the page
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Use the FileIcon enum instead of a raw string
                Icon = FileIcon.Paperclip,
                Title = "Unicode Attachment",
                Contents = "This PDF contains an attached file with a Unicode filename."
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(attachment);

            // Save the PDF document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with Unicode attachment saved to '{outputPdf}'.");
    }
}
