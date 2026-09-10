using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfConverter resides here
using System.Drawing.Imaging;      // ImageFormat for PNG

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputDir  = "output_images";     // folder for PNGs
        const string filePrefix = "page_";             // e.g., page_1.png
        const string fileSuffix = ".png";

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfConverter implements IDisposable – wrap in using for deterministic cleanup
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF file to the converter
            converter.BindPdf(inputPdf);

            // Prepare internal structures for conversion
            converter.DoConvert();

            int pageNumber = 1;
            // Iterate over all pages; HasNextImage indicates another page image is available
            while (converter.HasNextImage())
            {
                // Build full path for the current page image
                string outputPath = Path.Combine(outputDir, $"{filePrefix}{pageNumber}{fileSuffix}");

                // Save the current page as PNG
                converter.GetNextImage(outputPath, ImageFormat.Png);

                pageNumber++;
            }
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}