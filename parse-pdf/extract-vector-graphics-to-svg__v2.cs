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
        // Page number to extract vector graphics from (1‑based indexing)
        const int pageNumber = 1;
        // Directory where extracted SVG files will be saved
        const string outputDir = "ExtractedSvgs";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Validate the requested page number
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {pageNumber}. Document has {doc.Pages.Count} pages.");
                    return;
                }

                // Retrieve the specific page (Aspose.Pdf uses 1‑based indexing)
                Page page = doc.Pages[pageNumber];

                // Check if the page actually contains vector graphics
                if (!page.HasVectorGraphics())
                {
                    Console.WriteLine($"Page {pageNumber} does not contain vector graphics.");
                    return;
                }

                // Create an SvgExtractor instance (default options)
                SvgExtractor extractor = new SvgExtractor();

                // Extract each vector graphic on the page to a separate SVG file
                // The method creates one SVG file per graphic inside the target directory
                extractor.Extract(page, outputDir);

                Console.WriteLine($"Vector graphics from page {pageNumber} have been extracted to '{outputDir}'.");
            }
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"Aspose.Pdf error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}