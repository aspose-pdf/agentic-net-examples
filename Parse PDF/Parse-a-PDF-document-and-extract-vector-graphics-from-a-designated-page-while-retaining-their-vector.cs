using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: input PDF path, page number (1‑based), output directory for SVG files
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: <program> <input.pdf> <pageNumber> <outputDirectory>");
            return;
        }

        string inputPdfPath = args[0];
        string pageNumberArg = args[1];
        string outputDir = args[2];

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        if (!int.TryParse(pageNumberArg, out int pageNumber) || pageNumber < 1)
        {
            Console.Error.WriteLine("Page number must be a positive integer (1‑based).");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Validate the requested page exists (pages are 1‑based)
            if (pageNumber > pdfDoc.Pages.Count)
            {
                Console.Error.WriteLine($"Page number {pageNumber} exceeds document page count ({pdfDoc.Pages.Count}).");
                return;
            }

            // Retrieve the designated page
            Page page = pdfDoc.Pages[pageNumber];

            // Check if the page contains vector graphics
            if (!page.HasVectorGraphics())
            {
                Console.WriteLine($"Page {pageNumber} does not contain vector graphics.");
                return;
            }

            // Use SvgExtractor to extract vector graphics as SVG files
            // This retains full vector fidelity.
            SvgExtractor extractor = new SvgExtractor();

            // Extract all SVG images from the page into the output directory
            // Each vector graphic will be saved as a separate .svg file.
            extractor.Extract(page, outputDir);

            Console.WriteLine($"Vector graphics from page {pageNumber} have been extracted to '{outputDir}'.");
        }
    }
}