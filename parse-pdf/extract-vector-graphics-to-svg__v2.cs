using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Input PDF path
        const string inputPdf = "input.pdf";

        // Page number to extract (1‑based indexing)
        const int pageNumber = 1;

        // Directory where individual SVG files will be saved
        const string outputDir = "ExtractedSvgs";

        // Validate input file
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document (wrapped in using for deterministic disposal)
            using (Document doc = new Document(inputPdf))
            {
                // Verify the requested page exists
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                {
                    Console.Error.WriteLine($"Page number {pageNumber} is out of range. Document has {doc.Pages.Count} pages.");
                    return;
                }

                // Get the specific page
                Page page = doc.Pages[pageNumber];

                // Create an SvgExtractor (default options)
                SvgExtractor extractor = new SvgExtractor();

                // Extract all vector graphics on the page to separate SVG files
                // The method creates one SVG file per vector graphic inside the specified directory
                extractor.Extract(page, outputDir);
            }

            Console.WriteLine($"Vector graphics from page {pageNumber} have been extracted to '{outputDir}'.");
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}