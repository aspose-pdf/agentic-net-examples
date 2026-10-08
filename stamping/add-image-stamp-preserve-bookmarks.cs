using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string stampPath = "stamp.png";
        const string outputPath = "output.pdf";

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

        // Load the PDF inside a using block to ensure proper disposal.
        using (Document doc = new Document(inputPath))
        {
            // Create an ImageStamp. Settings can be adjusted as needed.
            ImageStamp imgStamp = new ImageStamp(stampPath)
            {
                // Example positioning – center of the page.
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // Keep the original image size (ImageStamp has no Scale property).
                Background = false,
                // Set opacity if a translucent effect is desired.
                Opacity = 0.8
            };

            // Apply the stamp to each page individually.
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Saving the document preserves all existing bookmarks/outlines.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPath}'.");
    }
}
