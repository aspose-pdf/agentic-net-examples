using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPath = "output.pdf";

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create an ImageStamp and set absolute X/Y coordinates
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                // Disable alignment to use absolute positioning
                HorizontalAlignment = HorizontalAlignment.None,
                VerticalAlignment   = VerticalAlignment.None,
                // Exact position in points from the bottom‑left corner of the page
                XIndent = 150, // X coordinate
                YIndent = 300, // Y coordinate
                // Optional visual settings
                Background = false,
                Opacity    = 0.8
            };

            // Apply the stamp to the first page (change index for other pages)
            Page page = pdfDoc.Pages[1];
            page.AddStamp(imgStamp);

            // Save the modified PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Image stamp placed and saved to '{outputPath}'.");
    }
}