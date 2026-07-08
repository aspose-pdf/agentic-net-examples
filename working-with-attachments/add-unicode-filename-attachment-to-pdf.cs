using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string attachmentPath = "sample.txt";
        const string unicodeFileName = "示例文件.txt";

        if (!File.Exists(inputPdf) || !File.Exists(attachmentPath))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        // Load the source PDF (lifecycle: load)
        using (Document doc = new Document(inputPdf))
        {
            // Ensure there is at least one page
            Page page = doc.Pages[1];

            // Define the rectangle that will host the attachment icon
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create a file specification for the attachment
            FileSpecification fileSpec = new FileSpecification(attachmentPath);
            // Set the Unicode filename that PDF viewers should display
            fileSpec.UnicodeName = unicodeFileName;

            // Create the file attachment annotation using the page, rectangle, and file spec
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Optional: provide a tooltip / popup text
                Contents = "Attached file with Unicode name"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(attachment);

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Attachment added. Saved to '{outputPdf}'.");
    }
}