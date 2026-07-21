using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Input PDF to which the tiled watermark will be applied
        const string inputPdfPath = "input.pdf";
        // PDF file that contains the pattern page (first page will be used as the tile)
        const string patternPdfPath = "pattern.pdf";
        // Output PDF with the tiled watermark background
        const string outputPdfPath = "output_tiled_watermark.pdf";

        // Verify that the required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(patternPdfPath))
        {
            Console.Error.WriteLine($"Pattern PDF not found: {patternPdfPath}");
            return;
        }

        try
        {
            // Load the source document (the one to be watermarked)
            using (Document doc = new Document(inputPdfPath))
            // Load the pattern document (contains the page that will be used as a tile)
            using (Document patternDoc = new Document(patternPdfPath))
            {
                // Retrieve the first page of the pattern document.
                // Aspose.Pdf uses 1‑based indexing for pages.
                Page patternPage = patternDoc.Pages[1];

                // Iterate over every page of the target document and add a WatermarkArtifact.
                foreach (Page page in doc.Pages)
                {
                    // Create a new WatermarkArtifact instance.
                    WatermarkArtifact watermark = new WatermarkArtifact();

                    // Set the artifact to be placed behind the page contents.
                    watermark.IsBackground = true;

                    // Optional: set opacity (0.0 = fully transparent, 1.0 = fully opaque).
                    watermark.Opacity = 0.5f;

                    // Use the pattern page as the content of the artifact.
                    // This embeds the pattern as a reusable XForm.
                    watermark.SetPdfPage(patternPage);

                    // Add the artifact to the current page.
                    page.Artifacts.Add(watermark);
                }

                // Save the modified document. Document.Save without SaveOptions writes PDF.
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Tiled watermark applied successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}