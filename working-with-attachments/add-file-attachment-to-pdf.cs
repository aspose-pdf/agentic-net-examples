using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // existing PDF
        const string outputPdf = "output_with_attachment.pdf";
        const string attachFile = "attachment.txt";    // file to embed

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(attachFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachFile}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the attachment annotation will appear (first page)
            Page page = doc.Pages[1]; // 1‑based indexing

            // Define the rectangle that represents the annotation's border
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550);

            // Create a FileSpecification for the file to be attached
            FileSpecification fileSpec = new FileSpecification(attachFile, "Sample attachment");

            // Create the FileAttachmentAnnotation using the page, rectangle, and file spec
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Optional: tooltip text shown when hovering
                Contents = "Attached file: attachment.txt"
                // Icon property can be set with a string value if desired, e.g.:
                // Icon = "Paperclip"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(attachment);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with attachment: {outputPdf}");
    }
}
