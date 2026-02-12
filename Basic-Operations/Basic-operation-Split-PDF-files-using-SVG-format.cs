using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector; // Contains SvgExtractor

class Program
{
    static void Main(string[] args)
    {
        // Path to the source PDF
        const string inputPdfPath = "input.pdf";

        // Directory where each page will be saved as an SVG file
        const string outputDirectory = "SvgPages";

        // Verify that the PDF file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Create an SVG extractor instance
            SvgExtractor svgExtractor = new SvgExtractor();

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDocument.Pages.Count; pageIndex++)
            {
                Page page = pdfDocument.Pages[pageIndex];

                // Build the SVG file name for the current page
                string svgFilePath = Path.Combine(outputDirectory, $"page_{pageIndex}.svg");

                // Extract the page content to an SVG file
                svgExtractor.Extract(page, svgFilePath);

                Console.WriteLine($"Page {pageIndex} extracted to '{svgFilePath}'.");
            }
        }
        catch (Exception ex)
        {
            // Report any unexpected errors
            Console.Error.WriteLine($"Exception: {ex.Message}");
        }
    }
}