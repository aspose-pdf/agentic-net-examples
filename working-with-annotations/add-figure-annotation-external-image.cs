using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output_with_figure.pdf"; // result PDF
        const string imagePath = "picture.jpg";        // external image to attach

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

            // Define the rectangle that will contain the annotation icon
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550);

            // Create a FileSpecification that points to the external image file
            // This embeds the image into the PDF as an attached file.
            FileSpecification fileSpec = new FileSpecification(imagePath);

            // Create the FileAttachment annotation using the constructor that accepts
            // the page, rectangle, and file specification.
            FileAttachmentAnnotation fileAttachment = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Choose an icon that best represents a figure (Graph icon works well)
                Icon = FileIcon.Graph,

                // Optional: set a tooltip text that appears when the user hovers over the icon
                Contents = "Figure: external image attachment"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(fileAttachment);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with figure annotation: {outputPdf}");
    }
}
