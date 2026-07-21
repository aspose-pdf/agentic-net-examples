using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the raster image file (PNG, JPEG, etc.)
        const string imagePath = "sample.png";

        // Path where the resulting PDF will be saved
        const string outputPdf = "output.pdf";

        // Verify that the image file exists before proceeding
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            pdfDoc.Pages.Add();

            // Instantiate the Image object (represents a raster image)
            Image img = new Image();

            // Assign the image file to the Image object
            img.File = imagePath;

            // Add the Image to the first page's Paragraphs collection
            pdfDoc.Pages[1].Paragraphs.Add(img);

            // Save the PDF document to the specified output path
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with image saved to '{outputPdf}'.");
    }
}