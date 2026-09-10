using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampImagePath = "stamp.png";

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

        // Load the source PDF (embedded files are kept automatically)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages and add the image stamp
            foreach (Page page in doc.Pages)
            {
                // Create a fresh ImageStamp for each page to avoid side‑effects
                ImageStamp stamp = new ImageStamp(stampImagePath)
                {
                    // Optional visual settings
                    Background          = false,                     // stamp on top of content
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Bottom,
                    Opacity             = 0.5f,                      // 50% transparent
                    // Position can be fine‑tuned via margins or indents if needed
                };

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF; embedded files remain intact
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPath}'.");
    }
}