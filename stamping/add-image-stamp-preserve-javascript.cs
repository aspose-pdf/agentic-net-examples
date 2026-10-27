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

        // Verify required files exist
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPath))
        {
            // Create an ImageStamp – this does NOT affect existing JavaScript actions
            ImageStamp imgStamp = new ImageStamp(stampPath)
            {
                // Example positioning – adjust as needed
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // Make the stamp appear on top of page content
                Background = false,
                // Optional opacity
                Opacity = 0.8
            };

            // Apply the stamp to each page individually (PageCollection has no AddStamp method)
            foreach (Page page in pdfDocument.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF; existing JavaScript actions are retained automatically
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPath}'.");
    }
}