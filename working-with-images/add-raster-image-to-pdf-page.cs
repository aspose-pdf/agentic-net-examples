using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the raster image file (PNG, JPEG, etc.)
        const string imagePath = "image.png";

        // Path where the resulting PDF will be saved
        const string outputPdf = "output.pdf";

        // Verify that the image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document and ensure it is disposed properly
        using (Document pdfDoc = new Document())
        {
            // Add a blank page to the document (pages are 1‑based)
            pdfDoc.Pages.Add();

            // Create an Image object and assign the image file
            Image img = new Image();
            img.File = imagePath;

            // Add the image to the first page's Paragraphs collection
            pdfDoc.Pages[1].Paragraphs.Add(img);

            // Save the PDF document to the specified output path
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with image saved to '{outputPdf}'.");
    }
}