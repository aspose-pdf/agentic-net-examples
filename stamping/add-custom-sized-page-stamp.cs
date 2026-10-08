using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string stampPath  = "stamp.png";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(stampPath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPath))
        {
            // Create an ImageStamp with custom dimensions
            ImageStamp imgStamp = new ImageStamp(stampPath)
            {
                Width  = 200,   // custom width in points
                Height = 100,   // custom height in points
                // Position the stamp within the page region
                LeftMargin = 50,   // distance from the left edge
                TopMargin  = 700,  // distance from the bottom edge
                Background = false, // place stamp over page content
                HorizontalAlignment = HorizontalAlignment.None,
                VerticalAlignment   = VerticalAlignment.None
            };

            // Target a specific page (e.g., page 2)
            int targetPageNumber = 2;
            if (targetPageNumber >= 1 && targetPageNumber <= pdfDocument.Pages.Count)
            {
                pdfDocument.Pages[targetPageNumber].AddStamp(imgStamp);
            }

            // Save the modified PDF
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Stamp applied and saved to '{outputPath}'.");
    }
}