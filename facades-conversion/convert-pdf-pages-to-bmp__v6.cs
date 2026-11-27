using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;          // for Resolution
using System.Drawing.Imaging;    // for ImageFormat

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "PagesBmp";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // PdfConverter implements IDisposable; ensure proper disposal
        using (PdfConverter converter = new PdfConverter())
        {
            // Load and bind the PDF document
            converter.BindPdf(inputPdf);

            // Configure conversion settings
            converter.StartPage = 1;
            converter.EndPage   = converter.PageCount; // convert all pages
            converter.Resolution = new Resolution(150); // DPI – adjust as needed
            // Optional: choose which page box to use (CropBox or MediaBox)
            // converter.CoordinateType = PageCoordinateType.CropBox;

            // Prepare the conversion – no arguments required
            converter.DoConvert();

            int pageNumber = converter.StartPage;
            while (converter.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.bmp");
                // GetNextImage writes the current page as the specified image format
                converter.GetNextImage(outPath, ImageFormat.Bmp);
                Console.WriteLine($"Saved page {pageNumber} as BMP to {outPath}");
                pageNumber++;
            }
        }
    }
}
