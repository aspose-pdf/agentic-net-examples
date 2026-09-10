using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the raster image file (PNG, JPEG, etc.)
        const string imagePath = "image.png";

        // Desired output PDF file path
        const string outputPdf = "output.pdf";

        // Verify that the image file exists before proceeding
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Create a new PDF document and ensure deterministic disposal
        using (Document pdfDoc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            pdfDoc.Pages.Add();

            // Instantiate the Image object (default constructor)
            Image img = new Image();

            // Set the source file for the image
            img.File = imagePath;

            // Add the image to the first page's Paragraphs collection
            pdfDoc.Pages[1].Paragraphs.Add(img);

            // Save the PDF document to the specified file
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with raster image saved to '{outputPdf}'.");
    }
}