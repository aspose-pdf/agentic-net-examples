using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF to which the tiled watermark will be applied
        const string inputPdfPath = "input.pdf";
        // PDF containing the pattern page to be used as a watermark
        const string patternPdfPath = "pattern.pdf";
        // Output PDF with the tiled watermark applied
        const string outputPdfPath = "output_watermarked.pdf";

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

        // Load the main document and the pattern document using Aspose.Pdf's Document constructor
        using (Document doc = new Document(inputPdfPath))
        using (Document patternDoc = new Document(patternPdfPath))
        {
            // Get the first page of the pattern PDF (Aspose.Pdf uses 1‑based indexing)
            Page patternPage = patternDoc.Pages[1];

            // Apply the watermark artifact to every page of the main document
            foreach (Page page in doc.Pages)
            {
                // Create a new WatermarkArtifact instance
                WatermarkArtifact watermark = new WatermarkArtifact();

                // Set the PDF page that will be tiled as the artifact content
                watermark.SetPdfPage(patternPage);

                // Place the artifact behind the page contents
                watermark.IsBackground = true;

                // Optional: set opacity (0.0 = fully transparent, 1.0 = fully opaque)
                watermark.Opacity = 0.5;

                // Add the artifact to the current page
                page.Artifacts.Add(watermark);
            }

            // Save the modified document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdfPath}'.");
    }
}