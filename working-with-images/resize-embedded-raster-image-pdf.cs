using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // existing PDF with an image
        const string outputPdfPath = "output.pdf";  // result PDF
        const string imagePath     = "image.png";   // raster image to embed

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Access the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create an Image object and set its source file
            Image img = new Image
            {
                File = imagePath,

                // Resize the raster image before saving
                // FixWidth / FixHeight control the displayed dimensions
                FixWidth  = 300,   // desired width in points
                FixHeight = 150    // desired height in points
            };

            // Add the resized image to the page's content
            page.Paragraphs.Add(img);

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Resized image PDF saved to '{outputPdfPath}'.");
    }
}