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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a background artifact for the page
                BackgroundArtifact bgArtifact = new BackgroundArtifact();

                // Assign the pattern image (file path)
                bgArtifact.SetImage(patternPath);

                // Place the artifact behind page contents
                bgArtifact.IsBackground = true;

                // Set opacity to 5 percent (0.05)
                bgArtifact.Opacity = 0.05;

                // Add the artifact to the page's artifact collection
                page.Artifacts.Add(bgArtifact);
            }

            // Save the modified PDF (lifecycle rule: use Save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with background pattern saved to '{outputPath}'.");
    }
}