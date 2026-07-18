using System;
using System.IO;
using System.Drawing.Imaging;               // ImageFormat enum
using Aspose.Pdf.Facades;               // PdfConverter facade
using Aspose.Pdf.Devices;               // Resolution

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputDir = "BmpImages";          // folder for BMP files

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfConverter implements IDisposable – use a using block for deterministic cleanup
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF file to the converter
            converter.BindPdf(inputPdf);

            // Configure conversion parameters BEFORE DoConvert()
            converter.Resolution = new Resolution(300); // 300 DPI
            // Note: CoordinateType enum is not available in the current Aspose.Pdf version;
            // the default coordinate system (MediaBox) is sufficient for most scenarios.

            // Initialise the conversion process
            converter.DoConvert();

            // Extract each page as a BMP image
            int pageNumber = 1;
            while (converter.HasNextImage())
            {
                string bmpPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");
                converter.GetNextImage(bmpPath, ImageFormat.Bmp); // save as BMP using System.Drawing.Imaging.ImageFormat
                pageNumber++;
            }
        }

        Console.WriteLine("PDF successfully converted to BMP images.");
    }
}
