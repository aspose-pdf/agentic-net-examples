using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Directory where individual SVG files will be saved
        const string outputRoot = "ExtractedVectors";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create an SvgExtractor instance (default options)
            SvgExtractor extractor = new SvgExtractor();

            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];

                // Check if the page contains any vector graphics
                if (!page.HasVectorGraphics())
                    continue; // Skip pages without vector graphics

                // Create a sub‑directory for this page's SVG files
                string pageOutputDir = Path.Combine(outputRoot, $"Page_{pageNum}");
                Directory.CreateDirectory(pageOutputDir);

                // Extract each vector graphic on the page to a separate SVG file
                // The method creates individual SVG files inside the specified directory
                extractor.Extract(page, pageOutputDir);
            }
        }

        Console.WriteLine($"Vector graphics extraction completed. SVG files are located in '{outputRoot}'.");
    }
}