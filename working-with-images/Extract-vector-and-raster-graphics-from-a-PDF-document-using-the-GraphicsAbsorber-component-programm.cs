using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Vector;

class ExtractGraphicsProgram
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";

        // Output directories for extracted graphics
        const string vectorOutputDir = "VectorGraphics";
        const string rasterOutputDir = "RasterImages";

        // Ensure output directories exist
        Directory.CreateDirectory(vectorOutputDir);
        Directory.CreateDirectory(rasterOutputDir);

        // Verify that the input PDF exists before attempting to load it.
        if (!File.Exists(inputPdfPath))
        {
            Console.WriteLine($"Error: The file '{inputPdfPath}' was not found. Please provide a valid PDF file and retry.");
            return; // Exit gracefully instead of throwing an unhandled exception.
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // Extract vector graphics (SVG) from each page using SvgExtractor
            // -----------------------------------------------------------------
            // SvgExtractor can extract all SVG images on a page to files.
            // It also works when the page contains path‑construction operators.
            Aspose.Pdf.Vector.SvgExtractor svgExtractor = new Aspose.Pdf.Vector.SvgExtractor();

            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Aspose.Pdf.Page page = pdfDoc.Pages[pageIndex];

                // Check if the page actually contains vector graphics
                if (page.HasVectorGraphics())
                {
                    // Build a file name for the SVG output of this page
                    string svgFilePath = Path.Combine(vectorOutputDir, $"page_{pageIndex}.svg");

                    // Try to save the whole page's vector graphics as a single SVG file.
                    // TrySaveVectorGraphics returns false if no vector graphics are present.
                    bool saved = page.TrySaveVectorGraphics(svgFilePath);
                    if (!saved)
                    {
                        // Fallback: use SvgExtractor to extract any embedded SVG images.
                        // This extracts each SVG image on the page to separate files.
                        // The method creates files named "svg_0.svg", "svg_1.svg", etc.
                        svgExtractor.Extract(page, vectorOutputDir);
                    }
                }
            }

            // -----------------------------------------------------------------
            // Extract raster images using PdfExtractor (Facade API)
            // -----------------------------------------------------------------
            Aspose.Pdf.Facades.PdfExtractor imageExtractor = new Aspose.Pdf.Facades.PdfExtractor();

            // Bind the PDF document to the extractor
            imageExtractor.BindPdf(pdfDoc);

            // Set the page range to cover the whole document
            imageExtractor.StartPage = 1;
            imageExtractor.EndPage = pdfDoc.Pages.Count;

            // Extract images from the specified range
            imageExtractor.ExtractImage();

            int imageIndex = 1;
            while (imageExtractor.HasNextImage())
            {
                // Save each extracted image. The default format is JPEG.
                // You can specify a different format by using the overload that accepts ImageFormat.
                string imagePath = Path.Combine(rasterOutputDir, $"image_{imageIndex}.jpg");
                imageExtractor.GetNextImage(imagePath);
                imageIndex++;
            }

            // No need to call pdfDoc.Save() because we only performed extraction.
        }

        Console.WriteLine("Vector and raster graphics extraction completed.");
    }
}
