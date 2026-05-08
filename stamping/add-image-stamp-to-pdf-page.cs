using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampImagePath = "stamp.png";

        // Verify input files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF document (using ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least two pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The PDF does not contain a second page.");
                return;
            }

            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImagePath);
            // Set stamp quality to 100 %
            imgStamp.Quality = 100;
            // Set stamp opacity to 0.8 (80 %)
            imgStamp.Opacity = 0.8;

            // Add the stamp to page 2
            Page pageTwo = doc.Pages[2];
            pageTwo.AddStamp(imgStamp);

            // Save the modified PDF (saving without options writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image stamp applied to page 2 and saved as '{outputPath}'.");
    }
}