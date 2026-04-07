using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";
        const string stampImage = "logo.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImage}");
            return;
        }

        // Load the source PDF. Using block ensures proper disposal.
        using (Document doc = new Document(inputPath))
        {
            // Create an image stamp from the specified file.
            ImageStamp imgStamp = new ImageStamp(stampImage);

            // Position the stamp in the bottom‑right corner with a small margin.
            imgStamp.HorizontalAlignment = HorizontalAlignment.Right;
            imgStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            imgStamp.XIndent = 10;          // 10 points margin from the right edge when Right‑aligned
            imgStamp.YIndent = 10;          // 10 points margin from the bottom edge when Bottom‑aligned
            imgStamp.Opacity = 0.5;        // semi‑transparent so underlying content stays visible

            // Add the stamp to every page. Existing JavaScript actions are preserved.
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF. JavaScript actions remain intact.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image stamp added. Output saved to '{outputPath}'.");
    }
}
