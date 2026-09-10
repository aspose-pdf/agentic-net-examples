using System;
using System.IO;
using Aspose.Pdf; // Core API (Document, Page, BackgroundArtifact)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "output.pdf";         // result PDF
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
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Create a background artifact
                BackgroundArtifact bg = new BackgroundArtifact();

                // Set the image for the artifact (can use file path or stream)
                bg.SetImage(patternPath);

                // Make the artifact semi‑transparent (10 % opacity)
                bg.Opacity = 0.1;          // range 0.0 .. 1.0
                bg.IsBackground = true;   // place behind page content

                // Add the artifact to the page
                page.Artifacts.Add(bg);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Background pattern applied and saved to '{outputPath}'.");
    }
}