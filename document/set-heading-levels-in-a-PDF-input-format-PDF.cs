using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_headings.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal.
        using (Document doc = new Document(inputPath))
        {
            // Ensure auto‑tagging is enabled (default is true, but set explicitly for clarity).
            AutoTaggingSettings.Default.EnableAutoTagging = true;

            // Create a HeadingLevels instance and define font sizes for heading detection.
            // Font sizes must be provided in decreasing order.
            var headingLevels = new HeadingLevels();
            headingLevels.AddLevels(new double[] { 24.0, 18.0, 14.0 });

            // Assign the configured heading levels to the global AutoTaggingSettings.
            AutoTaggingSettings.Default.HeadingLevels = headingLevels;

            // Optionally, choose a specific recognition strategy (Heuristic in this example).
            AutoTaggingSettings.Default.HeadingRecognitionStrategy = HeadingRecognitionStrategy.Heuristic;

            // Save the document; auto‑tagging (including heading detection) occurs during save.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}