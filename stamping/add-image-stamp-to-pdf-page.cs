using System;
using System.IO;
using Aspose.Pdf;               // Core API
using Aspose.Pdf.Facades;      // For ImageStamp (inherits from Stamp)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampImage = "stamp.png";   // Path to the image to be used as stamp

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImage}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a second page.");
                return;
            }

            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImage);

            // Set stamp properties: 100% quality and 0.8 opacity
            imgStamp.Quality = 100;      // quality in percent (0..100)
            imgStamp.Opacity = 0.8;      // opacity range 0.0 (transparent) to 1.0 (opaque)

            // Optionally, position the stamp (here we center it on the page)
            imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
            imgStamp.VerticalAlignment   = VerticalAlignment.Center;

            // Add the stamp to page 2 (Aspose.Pdf uses 1‑based indexing)
            Page pageTwo = doc.Pages[2];
            pageTwo.AddStamp(imgStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPath}'.");
    }
}