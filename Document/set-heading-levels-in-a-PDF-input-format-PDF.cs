using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Configure heading levels for auto‑tagging.
        // The list contains font sizes (largest to smallest) that will be mapped to heading levels.
        var headingLevels = new HeadingLevels();
        headingLevels.AddLevels(new List<double> { 24, 18, 14 });

        // Enable auto‑tagging and assign the custom heading levels.
        AutoTaggingSettings.Default.EnableAutoTagging = true;
        AutoTaggingSettings.Default.HeadingLevels = headingLevels;
        // Optional: AutoTaggingSettings.Default.HeadingRecognitionStrategy = HeadingRecognitionStrategy.Auto;

        // Load the PDF and save it. The auto‑tagging settings are applied during the save operation.
        using (Document doc = new Document(inputPath))
        {
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom heading levels to '{outputPath}'.");
    }
}