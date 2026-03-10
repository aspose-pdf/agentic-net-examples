using System;
using System.IO;
using System.Drawing.Imaging; // For ImageFormat (PNG, JPEG, etc.)
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Vector; // Contains SvgExtractor

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDirectory = "GraphicsOutput";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDirectory);

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // -------------------------------------------------
            // 1. Extract raster images (e.g., charts saved as images)
            // -------------------------------------------------
            using (PdfExtractor imageExtractor = new PdfExtractor())
            {
                imageExtractor.BindPdf(inputPdfPath);
                imageExtractor.ExtractImage();

                int imageIndex = 0;
                while (imageExtractor.HasNextImage())
                {
                    string imageFileName = Path.Combine(outputDirectory, $"image_{++imageIndex}.png");
                    // Save each extracted image as PNG
                    imageExtractor.GetNextImage(imageFileName, ImageFormat.Png);
                }
            }

            // -------------------------------------------------
            // 2. Extract vector graphics (e.g., diagrams, charts drawn with PDF graphics)
            // -------------------------------------------------
            SvgExtractor svgExtractor = new SvgExtractor();

            int pageNumber = 1;
            foreach (Page page in pdfDocument.Pages)
            {
                // Extract all SVG strings from the current page
                var svgContents = svgExtractor.Extract(page);

                int svgIndex = 0;
                foreach (string svg in svgContents)
                {
                    string svgFileName = Path.Combine(outputDirectory,
                        $"page_{pageNumber}_graphic_{++svgIndex}.svg");
                    File.WriteAllText(svgFileName, svg);
                }

                pageNumber++;
            }
        }

        Console.WriteLine("Graphical element detection completed. Results saved to: " + outputDirectory);
    }
}