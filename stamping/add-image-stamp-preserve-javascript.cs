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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create an image stamp
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                // Position the stamp at the bottom‑right corner
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Bottom,
                // Make the stamp semi‑transparent
                Opacity = 0.5f,
                // Add a uniform margin around the stamp
                LeftMargin   = 10,
                RightMargin  = 10,
                TopMargin    = 10,
                BottomMargin = 10
            };

            // Add the stamp to every page; existing JavaScript actions are preserved
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}