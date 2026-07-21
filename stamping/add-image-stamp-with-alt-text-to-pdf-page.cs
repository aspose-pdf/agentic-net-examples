using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampImage = "logo.png";          // image to be used as stamp
        const string altText    = "Company logo for accessibility";

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

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a third page.");
                return;
            }

            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImage);

            // Set alternative text for the stamp (accessibility)
            imgStamp.AlternativeText = altText;

            // Optional: position the stamp (example: top‑right corner)
            imgStamp.HorizontalAlignment = HorizontalAlignment.Right;
            imgStamp.VerticalAlignment   = VerticalAlignment.Top;
            imgStamp.Opacity = 0.8; // semi‑transparent

            // Add the stamp to page three (pages are 1‑based)
            Page pageThree = doc.Pages[3];
            pageThree.AddStamp(imgStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image stamp with alt text added to page 3 and saved as '{outputPath}'.");
    }
}