using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF and watermark image paths
        const string inputPdfPath   = "input.pdf";
        const string watermarkPath  = "watermark.png";
        const string outputPdfPath  = "watermarked_output.pdf";

        // Opacity for the watermark (0 = fully transparent, 1 = fully opaque)
        // This could be read from a configuration source; here it is hard‑coded.
        const double watermarkOpacity = 0.35;

        // Verify that the required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(watermarkPath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Iterate over all pages (1‑based indexing is handled internally by the foreach)
                foreach (Page page in pdfDoc.Pages)
                {
                    // Create an ImageStamp for the watermark image
                    ImageStamp watermarkStamp = new ImageStamp(watermarkPath)
                    {
                        // Place the watermark at the center of the page
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment   = VerticalAlignment.Center,

                        // Ensure the stamp is drawn over page content
                        Background = false,

                        // Set the desired opacity (0‑1 range)
                        Opacity = watermarkOpacity
                    };

                    // Add the stamp to the current page
                    page.AddStamp(watermarkStamp);
                }

                // Save the modified PDF
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Watermarked PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}