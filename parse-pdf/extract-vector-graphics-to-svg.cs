using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDirectory = "VectorGraphics";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDirectory);

        // Load the PDF document (using the recommended lifecycle rule)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure the extractor to create a separate SVG for each sub‑path
            SvgExtractionOptions extractionOptions = new SvgExtractionOptions
            {
                ExtractEverySubPathToSvg = true
            };
            SvgExtractor extractor = new SvgExtractor(extractionOptions);

            // Iterate pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Create a sub‑folder for each page to keep files organized
                string pageFolder = Path.Combine(outputDirectory, $"Page_{pageIndex}");
                Directory.CreateDirectory(pageFolder);

                // Extract all vector graphics on the page to individual SVG files
                extractor.Extract(page, pageFolder);
            }
        }

        Console.WriteLine("Vector graphics extraction completed.");
    }
}