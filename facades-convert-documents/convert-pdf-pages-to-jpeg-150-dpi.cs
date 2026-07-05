using System;
using System.IO;
using System.Drawing.Imaging; // for ImageFormat
using Aspose.Pdf.Facades; // PdfConverter
using Aspose.Pdf.Devices; // Resolution

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "Images";

        // Verify source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // PdfConverter implements IDisposable – use a using block for deterministic cleanup
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF
            converter.BindPdf(inputPdfPath);

            // Convert only pages 1 through 10
            converter.StartPage = 1;
            converter.EndPage   = 10;

            // Set resolution to 150 DPI
            converter.Resolution = new Resolution(150);

            // Use CropBox coordinates (default is CropBox, so we can omit explicit setting if the enum is unavailable)
            // converter.CoordinateType = CoordinateType.CropBox; // Removed – not present in older versions

            // Prepare the converter
            converter.DoConvert();

            int pageIndex = 1;
            while (converter.HasNextImage())
            {
                // Build output file name (e.g., page_1.jpg, page_2.jpg, ...)
                string outputPath = Path.Combine(outputFolder, $"page_{pageIndex}.jpg");

                // Save the current page as JPEG using System.Drawing.Imaging.ImageFormat
                converter.GetNextImage(outputPath, ImageFormat.Jpeg);

                pageIndex++;
            }
        }

        Console.WriteLine("Conversion of pages 1‑10 to JPEG (150 DPI, CropBox) completed.");
    }
}
