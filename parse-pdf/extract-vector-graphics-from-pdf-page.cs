using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "ExtractedSvgs";
        const int pageNumber = 1; // page to extract (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document (wrapped in using for deterministic disposal)
            using (Document doc = new Document(inputPath))
            {
                // Validate the requested page number
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {pageNumber}. Document has {doc.Pages.Count} pages.");
                    return;
                }

                // Retrieve the specific page (Aspose.Pdf uses 1‑based indexing)
                Page page = doc.Pages[pageNumber];

                // Extract each vector graphic on the page to a separate SVG file
                SvgExtractor extractor = new SvgExtractor();
                extractor.Extract(page, outputDir);

                Console.WriteLine($"Vector graphics from page {pageNumber} saved to '{outputDir}'.");
            }
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"PDF error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}