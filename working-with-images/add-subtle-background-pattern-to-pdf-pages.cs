using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string patternPath = "pattern.png";

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Apply the background artifact to each page
            foreach (Page page in doc.Pages)
            {
                // Create a background artifact
                BackgroundArtifact bg = new BackgroundArtifact();

                // Set the pattern image (file path)
                bg.SetImage(patternPath);

                // Set opacity to 5% (0.05)
                bg.Opacity = 0.05;

                // Ensure the artifact is placed behind page content
                bg.IsBackground = true;

                // Add the artifact to the page
                page.Artifacts.Add(bg);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Background pattern applied and saved to '{outputPath}'.");
    }
}