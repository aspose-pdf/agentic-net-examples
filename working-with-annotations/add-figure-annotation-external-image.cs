using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF and external image file paths
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imageFilePath = "image.png";

        // Ensure the files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imageFilePath))
        {
            Console.Error.WriteLine($"Image file not found: {imageFilePath}");
            return;
        }

        // Load the PDF, add a FileAttachment annotation that shows the image as its appearance, and save
        using (Document doc = new Document(inputPdfPath))
        {
            // Choose the page where the annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle for the annotation (coordinates are in points; lower‑left origin)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create a FileSpecification that points to the external image file
            FileSpecification fileSpec = new FileSpecification(imageFilePath);

            // Create the FileAttachment annotation using the page, rectangle, and file specification
            FileAttachmentAnnotation fileAttachment = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Optional: choose an icon to represent the attachment
                Icon = FileIcon.Graph,
                // Optional: set a tooltip text that appears when the user hovers over the annotation
                Contents = "Attached image"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(fileAttachment);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with image attachment saved to '{outputPdfPath}'.");
    }
}