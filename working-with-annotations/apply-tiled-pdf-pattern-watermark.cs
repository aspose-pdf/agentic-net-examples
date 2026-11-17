using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // WatermarkArtifact

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";      // PDF to be watermarked
        const string patternPdfPath = "pattern.pdf";    // PDF containing the pattern (single page)
        const string outputPdfPath  = "watermarked.pdf";

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

        // Load the source document
        using (Document doc = new Document(inputPdfPath))
        {
            // Load the PDF pattern (single‑page PDF used as a tile)
            Document patternDoc = new Document(patternPdfPath);
            // Ensure the pattern PDF has at least one page
            if (patternDoc.Pages.Count == 0)
            {
                Console.Error.WriteLine("Pattern PDF does not contain any pages.");
                return;
            }
            Page patternPage = patternDoc.Pages[1];

            // Dimensions of the pattern page (used for tiling calculations)
            double patternWidth  = patternPage.PageInfo.Width;
            double patternHeight = patternPage.PageInfo.Height;

            // Apply the tiled watermark to every page
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Tile the pattern across the whole page
                for (double y = 0; y < pageHeight; y += patternHeight)
                {
                    for (double x = 0; x < pageWidth; x += patternWidth)
                    {
                        var watermark = new WatermarkArtifact
                        {
                            // Use the pattern page as the source for the artifact
                            // SetPdfPage copies the visual content of the supplied page
                            Opacity = 0.15,          // semi‑transparent
                            IsBackground = true,    // draw behind page content
                            Position = new Point(x, y)
                        };
                        watermark.SetPdfPage(patternPage);
                        page.Artifacts.Add(watermark);
                    }
                }
            }

            // Save the result
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdfPath}'.");
    }
}
