using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string watermarkImage = "watermark.png";
        const string outputPdf = "watermarked.pdf";

        // Verify input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(watermarkImage))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImage}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdf))
        {
            // Apply the watermark to every page in the document
            foreach (Aspose.Pdf.Page page in doc.Pages)
            {
                // Create an ImageStamp that will act as a watermark
                Aspose.Pdf.ImageStamp stamp = new Aspose.Pdf.ImageStamp(watermarkImage)
                {
                    // Center the stamp on the page
                    HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center,
                    VerticalAlignment   = Aspose.Pdf.VerticalAlignment.Center,
                    // Make the image semi‑transparent so it behaves like a watermark
                    Opacity   = 0.3f,
                    // Draw the stamp behind existing page content
                    Background = true,
                    // Optionally stretch the image to cover the whole page
                    Width  = page.PageInfo.Width,
                    Height = page.PageInfo.Height
                };

                page.AddStamp(stamp);
            }

            // Save the watermarked PDF (PDF format does not require explicit SaveOptions)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}
