using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputFolder  = "BmpImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Initialize the PdfConverter facade
        using (Aspose.Pdf.Facades.PdfConverter converter = new Aspose.Pdf.Facades.PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdfPath);

            // Configure resolution (e.g., 300 DPI)
            converter.Resolution = new Aspose.Pdf.Devices.Resolution(300);

            // Set the page coordinate type (CropBox is default, can be changed to Media)
            converter.CoordinateType = Aspose.Pdf.PageCoordinateType.CropBox;

            // Perform initial conversion setup
            converter.DoConvert();

            int pageIndex = 1;
            // Extract each page as a BMP image
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageIndex}.bmp");
                converter.GetNextImage(outputPath, ImageFormat.Bmp);
                pageIndex++;
            }
        }

        Console.WriteLine("PDF conversion to BMP images completed.");
    }
}