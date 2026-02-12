using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "output_svg";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Create an SVG extractor instance
            SvgExtractor extractor = new SvgExtractor();

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDocument.Pages.Count; pageIndex++)
            {
                Page page = pdfDocument.Pages[pageIndex];

                // Extract SVG content of the current page to a file
                string svgFilePath = Path.Combine(outputFolder, $"page_{pageIndex}.svg");
                extractor.Extract(page, svgFilePath);

                Console.WriteLine($"Page {pageIndex} extracted to '{svgFilePath}'.");
            }

            Console.WriteLine("PDF split into SVG files successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}