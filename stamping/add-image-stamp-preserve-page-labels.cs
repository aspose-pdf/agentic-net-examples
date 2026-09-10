using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "stamped_output.pdf";
        const string stampImagePath = "stamp.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF document (1‑based page indexing)
        using (Document pdfDocument = new Document(inputPath))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                // Position the stamp at the bottom‑right corner
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Bottom,
                // Optional: set size, opacity, background
                // Width = 100,
                // Height = 50,
                Opacity = 0.5,          // semi‑transparent
                Background = false      // stamp on top of page content
            };

            // Apply the stamp to each page individually
            foreach (Page page in pdfDocument.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF; page labels remain unchanged
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Image stamp added. Output saved to '{outputPath}'.");
    }
}