using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Directory where extracted SVG files will be saved
        const string outputDirectory = "VectorGraphics";
        // Page number to extract (1‑based indexing)
        const int pageNumber = 2;

        // Validate input file
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Verify the requested page exists
            if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
            {
                Console.Error.WriteLine($"Page number {pageNumber} is out of range. Document has {pdfDoc.Pages.Count} pages.");
                return;
            }

            // Retrieve the specified page (1‑based indexing)
            Page page = pdfDoc.Pages[pageNumber];

            // Check whether the page contains vector graphics
            if (!page.HasVectorGraphics())
            {
                Console.WriteLine($"Page {pageNumber} does not contain vector graphics.");
                return;
            }

            // Build the output SVG file path
            string svgOutputPath = Path.Combine(outputDirectory, $"page_{pageNumber}_vector.svg");

            // Try to save the vector graphics as SVG; the method returns true on success
            bool saved = page.TrySaveVectorGraphics(svgOutputPath);
            if (saved)
            {
                Console.WriteLine($"Vector graphics extracted to: {svgOutputPath}");
            }
            else
            {
                Console.WriteLine($"Failed to extract vector graphics from page {pageNumber}.");
            }
        }
    }
}