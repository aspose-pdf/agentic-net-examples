using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF
        const string outputPath = "output.pdf";  // result PDF
        const string stampImage = "stamp.png";   // image to use as stamp

        // Verify files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImage}");
            return;
        }

        // Load the PDF (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Page indexing in Aspose.Pdf is 1‑based; page 2 is the second page
            Page page = doc.Pages[2];

            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImage);

            // Set required properties: 100 % quality and 0.8 opacity
            imgStamp.Quality = 100;   // quality in percent (0‑100)
            imgStamp.Opacity = 0.8;   // opacity range 0.0‑1.0

            // Add the stamp to the selected page
            page.AddStamp(imgStamp);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image stamp added to page 2 and saved as '{outputPath}'.");
    }
}