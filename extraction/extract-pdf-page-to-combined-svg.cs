using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "SvgOutput";

        // Verify input PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        using (var doc = new Document(inputPdf))
        {
            // Page numbers are 1‑based for users
            const int pageNumber = 17;
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Get the requested page (Aspose.Pdf uses 1‑based indexing for the Pages collection)
            Page page = doc.Pages[pageNumber];

            // Use SvgExtractor with default options to export the whole page as a combined SVG
            var extractionOptions = new SvgExtractionOptions(); // default options
            var extractor = new SvgExtractor(extractionOptions);

            // The extractor creates files in the specified folder; the base name is used for the files.
            string basePath = Path.Combine(outputFolder, $"page{pageNumber}");
            extractor.Extract(page, basePath);

            Console.WriteLine($"Page {pageNumber} extracted as SVG files to folder: {outputFolder}");
        }
    }
}
