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

        if (!File.Exists(inputPath) || !File.Exists(patternPath))
        {
            Console.Error.WriteLine("Input PDF or pattern image not found.");
            return;
        }

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based (rule: page-indexing-one-based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a background artifact, set the image and opacity
                BackgroundArtifact bg = new BackgroundArtifact();
                bg.SetImage(patternPath);   // load image from file
                bg.Opacity = 0.05;          // 5 percent opacity
                bg.IsBackground = true;    // place behind page contents

                // Add the artifact to the page
                page.Artifacts.Add(bg);
            }

            // Save the modified PDF (rule: document-disposal-with-using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Background pattern applied and saved to '{outputPath}'.");
    }
}