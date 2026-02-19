using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class SplitPdfToSvg
{
    static void Main(string[] args)
    {
        // Input PDF file path (change as needed)
        const string pdfPath = "input.pdf";

        // Directory where individual SVG files will be saved
        const string outputDirectory = "output_svgs";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(pdfPath);

            // Ensure the output directory exists
            if (!Directory.Exists(outputDirectory))
                Directory.CreateDirectory(outputDirectory);

            // Create an extractor for SVG images/graphics
            SvgExtractor svgExtractor = new SvgExtractor();

            // Iterate through each page and extract it as a separate SVG file
            int pageIndex = 1;
            foreach (Page page in pdfDocument.Pages)
            {
                string svgFilePath = Path.Combine(outputDirectory, $"page_{pageIndex}.svg");
                // Extract the page content to an SVG file
                svgExtractor.Extract(page, svgFilePath);
                Console.WriteLine($"Page {pageIndex} extracted to '{svgFilePath}'.");
                pageIndex++;
            }

            Console.WriteLine("PDF split into SVG files successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}