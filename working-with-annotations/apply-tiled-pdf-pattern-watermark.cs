using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";      // PDF to receive the tiled background
        const string patternPdfPath = "pattern.pdf";    // PDF containing the pattern page
        const string outputPdfPath  = "output.pdf";

        if (!File.Exists(inputPdfPath) || !File.Exists(patternPdfPath))
        {
            Console.Error.WriteLine("Input or pattern PDF not found.");
            return;
        }

        // Load the document that will receive the watermark artifact
        using (Document targetDoc = new Document(inputPdfPath))
        // Load the document that provides the pattern page
        using (Document patternDoc = new Document(patternPdfPath))
        {
            // Assume the first page of the pattern PDF is the tile to repeat
            Page patternPage = patternDoc.Pages[1];

            // Create a WatermarkArtifact instance
            WatermarkArtifact watermark = new WatermarkArtifact();

            // Use the pattern page as the content of the artifact
            watermark.SetPdfPage(patternPage);

            // Place the artifact behind page contents and make it semi‑transparent
            watermark.IsBackground = true;
            watermark.Opacity = 0.5; // 0 = fully transparent, 1 = fully opaque

            // Add the same artifact to every page of the target document.
            // The artifact will be tiled automatically because it represents a PDF page.
            foreach (Page page in targetDoc.Pages)
            {
                page.Artifacts.Add(watermark);
            }

            // Save the modified document
            targetDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Tiled watermark artifact applied and saved to '{outputPdfPath}'.");
    }
}