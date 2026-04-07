using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "attachment.png"; // external image to attach

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the annotation will be placed
            Page page = doc.Pages[1]; // 1‑based indexing

            // Define the rectangle for the annotation (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create a FileSpecification that points to the external image file
            FileSpecification fileSpec = new FileSpecification(imagePath);

            // Create the FileAttachment annotation with the file specification
            FileAttachmentAnnotation fileAttachment = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Optional: set an icon to represent the attachment
                Icon = FileIcon.Graph,
                // Optional: provide a tooltip / title
                Title = "Attached Image",
                // Optional: description shown in the popup
                Contents = "This annotation embeds an external image file."
            };

            // Add the annotation to the page
            page.Annotations.Add(fileAttachment);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Figure (file attachment) annotation added and saved to '{outputPdf}'.");
    }
}
