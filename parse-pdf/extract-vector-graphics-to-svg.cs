using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputRoot = "ExtractedSvgs";

        // Verify the source PDF exists.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the root output directory exists.
        Directory.CreateDirectory(outputRoot);

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdf))
        {
            // SvgExtractor handles conversion of vector graphics to SVG.
            SvgExtractor extractor = new SvgExtractor();

            // Iterate through all pages (1‑based indexing).
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Skip pages that contain no vector graphics.
                if (!page.HasVectorGraphics())
                    continue;

                // Create a sub‑folder for each page to avoid filename clashes.
                string pageFolder = Path.Combine(outputRoot, $"Page_{pageIndex}");
                Directory.CreateDirectory(pageFolder);

                // Extract each vector graphic on the page to a separate SVG file.
                // The method creates files inside the supplied directory.
                extractor.Extract(page, pageFolder);
            }
        }

        Console.WriteLine("All vector graphics have been extracted to SVG files.");
    }
}