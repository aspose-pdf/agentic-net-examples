using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output_with_bg.pdf"; // result PDF
        const string patternImg = "pattern.png";       // background pattern image

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(patternImg))
        {
            Console.Error.WriteLine($"Pattern image not found: {patternImg}");
            return;
        }

        // Load the PDF document (using rule: document-is-tagged-does-not-exist)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Create a background artifact
                BackgroundArtifact bgArtifact = new BackgroundArtifact();

                // Set the image for the artifact (pattern)
                bgArtifact.SetImage(patternImg);

                // Set opacity to 10 percent (0.1)
                bgArtifact.Opacity = 0.1;

                // Ensure the artifact is placed behind page content
                bgArtifact.IsBackground = true;

                // Add the artifact to the page
                page.Artifacts.Add(bgArtifact);
            }

            // Save the modified PDF (using rule: document-disposal-with-using)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Background pattern added. Saved to '{outputPdf}'.");
    }
}