using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Drawing;            // For Image handling (used by WatermarkArtifact)

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";      // Source PDF containing attachments
        const string outputPdf     = "output.pdf";     // Destination PDF after watermarking
        const string watermarkImg  = "watermark.png";  // Watermark image to apply

        // Ensure required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(watermarkImg))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImg}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Create a new watermark artifact for the current page
                WatermarkArtifact watermark = new WatermarkArtifact();

                // Set the watermark image (the image file is read internally)
                watermark.SetImage(watermarkImg);

                // Configure appearance – place behind page content and make semi‑transparent
                watermark.IsBackground = true;
                watermark.Opacity      = 0.3;   // 30 % opacity

                // Add the watermark to the page's artifact collection
                page.Artifacts.Add(watermark);
            }

            // Save the modified document (PDF format) using the standard Save method
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}