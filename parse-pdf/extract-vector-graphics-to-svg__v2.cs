using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPdf = "input.pdf";

        // 1‑based page number to extract graphics from
        const int pageNumber = 1;

        // Directory where individual SVG files will be saved
        const string outputDir = "ExtractedSvgs";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create the output directory if it does not exist
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Ensure the requested page number is valid (Aspose.Pdf uses 1‑based indexing)
            if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} is out of range. Document has {pdfDoc.Pages.Count} pages.");
                return;
            }

            // Create an SvgExtractor instance (default options)
            SvgExtractor extractor = new SvgExtractor();

            // Extract all vector graphics from the specified page.
            // Each graphic is saved as a separate SVG file inside outputDir.
            extractor.Extract(pdfDoc.Pages[pageNumber], outputDir);
        }

        Console.WriteLine($"Vector graphics extracted to directory: {outputDir}");
    }
}