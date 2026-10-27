using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPath     = "output.pdf";

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPath))
        {
            // Ensure the document has at least two pages (Aspose.Pdf uses 1‑based indexing)
            if (pdfDocument.Pages.Count < 2)
            {
                Console.Error.WriteLine("The PDF contains fewer than 2 pages.");
                return;
            }

            // Create an ImageStamp with the required quality and opacity
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                Quality = 100,   // 100 % JPEG quality (0‑100)
                Opacity = 0.8    // 80 % opacity (0‑1)
            };

            // Add the stamp to page two
            Page pageTwo = pdfDocument.Pages[2];
            pageTwo.AddStamp(imgStamp);

            // Save the modified PDF
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Image stamp added to page 2 and saved as '{outputPath}'.");
    }
}