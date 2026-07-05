using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // source PDF
        const string outputPath = "output.pdf";     // result PDF
        const string patternPath = "pattern.png";   // background pattern image

        // Ensure the input files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(patternPath))
        {
            Console.Error.WriteLine($"Pattern image not found: {patternPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Create a background artifact for the page
                BackgroundArtifact bgArtifact = new BackgroundArtifact();

                // Set the image for the artifact (can use a file path or a stream)
                bgArtifact.SetImage(patternPath);

                // Set opacity to 5 percent (0.05)
                bgArtifact.Opacity = 0.05;

                // Place the artifact behind the page content
                bgArtifact.IsBackground = true;

                // Add the artifact to the page's artifact collection
                page.Artifacts.Add(bgArtifact);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Background pattern applied and saved to '{outputPath}'.");
    }
}