using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input SVG file path
        const string svgPath = "input.svg";

        // Directory where JPEG images will be saved
        const string outputDir = "OutputImages";

        // Verify that the SVG file exists
        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the SVG file into a PDF document using SvgLoadOptions
        using (Document pdfDoc = new Document(svgPath, new SvgLoadOptions()))
        {
            // Initialize the PdfConverter facade with the loaded document
            using (PdfConverter converter = new PdfConverter(pdfDoc))
            {
                // Convert all pages (SVG is usually a single page, but this works for multi‑page SVGs)
                converter.StartPage = 1;
                converter.EndPage   = pdfDoc.Pages.Count;

                // Optional: set a higher resolution for better image quality
                // converter.Resolution = 300;

                // Prepare the converter for processing
                converter.DoConvert();

                int pageNumber = 1;
                // Iterate through each generated image and save it as JPEG (default format)
                while (converter.HasNextImage())
                {
                    string jpegPath = Path.Combine(outputDir, $"page_{pageNumber}.jpeg");
                    converter.GetNextImage(jpegPath); // Saves the current page as JPEG
                    pageNumber++;
                }
            }
        }

        Console.WriteLine($"SVG has been converted to JPEG images in '{outputDir}'.");
    }
}