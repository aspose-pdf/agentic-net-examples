using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "output_with_bg.pdf"; // result PDF
        const string patternPath = "pattern.png";       // background pattern image

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(patternPath))
        {
            Console.Error.WriteLine($"Pattern image not found: {patternPath}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a background artifact for the page
                BackgroundArtifact bgArtifact = new BackgroundArtifact();

                // Set the image that will be used as the background pattern
                bgArtifact.SetImage(patternPath);

                // Place the artifact behind page contents
                bgArtifact.IsBackground = true;

                // Set a subtle opacity (5 percent)
                bgArtifact.Opacity = 0.05;

                // Add the artifact to the page's artifact collection
                page.Artifacts.Add(bgArtifact);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with background pattern: {outputPath}");
    }
}