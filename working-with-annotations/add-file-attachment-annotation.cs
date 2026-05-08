using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // existing PDF
        const string outputPdf = "output.pdf";         // result PDF
        const string imagePath = "attachment.png";    // external image to attach

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the rectangle that bounds the annotation (coordinates in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create a FileSpecification that points to the external image file
            // The constructor takes a stream and a display name for the attached file
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                FileSpecification fileSpec = new FileSpecification(imgStream, Path.GetFileName(imagePath));

                // Create the FileAttachment annotation with the page, rectangle, and file specification
                FileAttachmentAnnotation fileAttachment = new FileAttachmentAnnotation(page, rect, fileSpec)
                {
                    // Choose an icon to represent the attachment (optional)
                    Icon = FileIcon.Graph,
                    // Optional tooltip text shown when hovering over the annotation
                    Contents = "Attached image file",
                    // Optional title displayed in the annotation's popup window
                    Title = "Image Attachment"
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(fileAttachment);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with file attachment saved to '{outputPdf}'.");
    }
}