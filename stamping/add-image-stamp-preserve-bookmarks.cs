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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the existing PDF (preserves bookmarks and outline)
        using (Document doc = new Document(inputPath))
        {
            // Create an image stamp
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                // Position the stamp at the bottom‑right of each page
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Bottom,
                Opacity = 0.5,               // semi‑transparent
                RightMargin = 10,
                BottomMargin = 10
            };

            // Apply the stamp to every page; bookmarks remain unchanged
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image stamp added. Output saved to '{outputPath}'.");
    }
}