using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfConverter facade
using System.Drawing.Imaging;      // ImageFormat for PNG

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputDir  = "output_images";      // folder for PNG files
        const string prefix     = "page_";              // filename prefix
        const string suffix     = ".png";               // PNG extension

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfConverter (facade) to convert each page to PNG
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF file to the converter
            converter.BindPdf(inputPdf);

            // Prepare internal structures for conversion
            converter.DoConvert();

            int pageNumber = 1;
            // Iterate through all pages; HasNextImage indicates more pages
            while (converter.HasNextImage())
            {
                // Build output file path with sequential naming
                string outputPath = Path.Combine(outputDir, $"{prefix}{pageNumber}{suffix}");

                // Save current page as PNG
                converter.GetNextImage(outputPath, ImageFormat.Png);

                pageNumber++;
            }
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}