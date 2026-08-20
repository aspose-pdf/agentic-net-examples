using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";      // PDF to receive the tiled watermark
        const string patternPdfPath = "pattern.pdf";    // PDF containing the pattern page
        const string outputPdfPath  = "output.pdf";

        // Verify that both source files exist
        if (!File.Exists(inputPdfPath) || !File.Exists(patternPdfPath))
        {
            Console.Error.WriteLine("Input PDF or pattern PDF not found.");
            return;
        }

        // Load the PDF that provides the pattern (assumed to have at least one page)
        using (Document patternDoc = new Document(patternPdfPath))
        {
            // Use the first page of the pattern document as the tile content
            Page patternPage = patternDoc.Pages[1];

            // Load the target document where the tiled watermark will be applied
            using (Document targetDoc = new Document(inputPdfPath))
            {
                // Apply the watermark artifact to every page in the target document
                foreach (Page page in targetDoc.Pages)
                {
                    // Create a new WatermarkArtifact for the current page
                    WatermarkArtifact watermark = new WatermarkArtifact();

                    // Set the PDF page that will be used as the tiled pattern
                    watermark.SetPdfPage(patternPage);

                    // Place the artifact behind the page content
                    watermark.IsBackground = true;

                    // Optional: adjust opacity (0.0 = fully transparent, 1.0 = opaque)
                    watermark.Opacity = 0.5;

                    // Add the artifact to the page's artifact collection
                    page.Artifacts.Add(watermark);
                }

                // Save the modified document (using the standard Document.Save method)
                targetDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Tiled watermark PDF saved to '{outputPdfPath}'.");
    }
}