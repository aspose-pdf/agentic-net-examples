using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string stampPath = "stamp.png";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampPath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampPath}");
            return;
        }

        // Load the PDF document.
        using (Document doc = new Document(inputPath))
        {
            // Create an image stamp with 30% opacity and set it as a background.
            ImageStamp stamp = new ImageStamp(stampPath)
            {
                Opacity = 0.3f,          // 30% opacity
                Background = true       // place behind existing content
            };

            // Apply the stamp to every page.
            foreach (Page page in doc.Pages)
            {
                // Optionally make the stamp cover the whole page.
                stamp.Width = page.PageInfo.Width;
                stamp.Height = page.PageInfo.Height;
                stamp.XIndent = 0;      // left offset
                stamp.YIndent = 0;      // bottom offset

                page.AddStamp(stamp);
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Background stamp applied and saved to '{outputPath}'.");
    }
}