using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string imagePath   = "sample.png";   // external image to attach
        const string outputPath  = "figure_annotation.pdf";

        // Ensure the image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a FileSpecification that points to the external image file
            // This embeds the file into the PDF and makes it available to the annotation.
            FileSpecification fileSpec = new FileSpecification(imagePath);

            // Define the rectangle where the annotation will appear (llx, lly, urx, ury)
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create the FileAttachment annotation using the page, rectangle, and file specification.
            FileAttachmentAnnotation fileAnn = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Choose an icon that will be displayed on the page.
                // Options: PushPin (default), Graph, Paperclip, Tag.
                Icon = FileIcon.Graph,

                // Optional: set a title and contents that appear in the popup.
                Title    = "Attached Image",
                Contents = "Click the icon to open the attached image."
            };

            // Add the annotation to the page's annotation collection.
            page.Annotations.Add(fileAnn);

            // Save the PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with figure (file attachment) annotation saved to '{outputPath}'.");
    }
}