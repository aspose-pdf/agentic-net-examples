using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // PDF to be watermarked
        const string patternPdf = "pattern.pdf";    // PDF containing the pattern page
        const string outputPdf  = "output.pdf";

        if (!File.Exists(inputPdf) || !File.Exists(patternPdf))
        {
            Console.Error.WriteLine("Input or pattern file not found.");
            return;
        }

        // Load the main document and the pattern document
        using (Document doc = new Document(inputPdf))
        using (Document patternDoc = new Document(patternPdf))
        {
            // Get the first page of the pattern PDF (1‑based indexing)
            Page patternPage = patternDoc.Pages[1];

            // Apply the watermark artifact to every page of the main document
            foreach (Page page in doc.Pages)
            {
                // Create a WatermarkArtifact instance
                WatermarkArtifact watermark = new WatermarkArtifact();

                // Place the artifact behind page contents
                watermark.IsBackground = true;

                // Use the pattern PDF page as the tiled background
                watermark.SetPdfPage(patternPage);

                // Optionally adjust opacity (0.0 – 1.0)
                watermark.Opacity = 0.5;

                // Add the artifact to the current page
                page.Artifacts.Add(watermark);
            }

            // Save the resulting PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}