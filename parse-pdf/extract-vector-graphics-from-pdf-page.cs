using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;   // Contains SvgExtractor

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string pdfPath = "input.pdf";

        // Page number to extract vector graphics from (1‑based indexing)
        const int pageNumber = 2;

        // Directory where the extracted SVG files will be saved
        const string outputDirectory = "ExtractedSvgs";

        // Validate input file existence
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Ensure the requested page exists
            if (pageNumber < 1 || pageNumber > pdfDocument.Pages.Count)
            {
                Console.Error.WriteLine($"Page number {pageNumber} is out of range. Document has {pdfDocument.Pages.Count} pages.");
                return;
            }

            // Retrieve the specific page (Aspose.Pdf uses 1‑based indexing)
            Page page = pdfDocument.Pages[pageNumber];

            // Check whether the page actually contains vector graphics
            if (!page.HasVectorGraphics())
            {
                Console.WriteLine($"Page {pageNumber} does not contain any vector graphics.");
                return;
            }

            // Ensure the output directory exists
            Directory.CreateDirectory(outputDirectory);

            // Create an SvgExtractor instance (default options)
            SvgExtractor extractor = new SvgExtractor();

            // Extract all vector graphics from the page into separate SVG files
            // The method creates one SVG file per graphic element inside the specified directory
            extractor.Extract(page, outputDirectory);

            Console.WriteLine($"Vector graphics from page {pageNumber} have been extracted to '{outputDirectory}'.");
        }
    }
}