using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampPath  = "stamp.png";

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

        // Load the original PDF (embedded files are part of the document and stay intact)
        using (Document doc = new Document(inputPath))
        {
            // Create an image stamp
            ImageStamp stamp = new ImageStamp(stampPath)
            {
                // Position the stamp in the bottom‑right corner with a small margin
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Bottom,
                XIndent = 10,               // offset from the right edge when Right‑aligned
                YIndent = 10,               // offset from the bottom edge when Bottom‑aligned
                Opacity = 0.5f             // semi‑transparent
            };

            // Apply the stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF; embedded files are preserved automatically
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPath}'.");
    }
}
