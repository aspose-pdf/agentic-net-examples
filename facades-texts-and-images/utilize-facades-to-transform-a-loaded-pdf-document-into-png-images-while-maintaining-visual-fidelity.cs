using System;
using System.IO;
using System.Drawing.Imaging;          // ImageFormat for PNG
using Aspose.Pdf;                     // Document
using Aspose.Pdf.Facades;             // PdfConverter
using Aspose.Pdf.Devices;             // Resolution

class PdfToPngConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output directory for PNG images
        const string outputDir = "PngPages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Initialize PdfConverter facade
                using (PdfConverter converter = new PdfConverter())
                {
                    // Bind the loaded document to the converter
                    converter.BindPdf(pdfDoc);

                    // Set a high resolution to preserve visual fidelity (e.g., 300 DPI)
                    converter.Resolution = new Resolution(300);

                    // Prepare the converter for conversion
                    converter.DoConvert();

                    int pageNumber = 1;
                    // Iterate over all pages and save each as a PNG image
                    while (converter.HasNextImage())
                    {
                        string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
                        // Save the current page image as PNG
                        converter.GetNextImage(outputPath, ImageFormat.Png);
                        pageNumber++;
                    }

                    // Release resources held by the converter
                    converter.Close();
                }
            }

            Console.WriteLine("PDF pages have been successfully converted to PNG images.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}
