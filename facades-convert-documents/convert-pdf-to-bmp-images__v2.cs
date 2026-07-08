using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;
using System.Drawing.Imaging;

class PdfToBmpConverter
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "BmpPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // PdfConverter implements IDisposable, so wrap it in a using block
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdfPath);

            // In recent Aspose.Pdf versions font substitution for missing fonts is enabled by default.
            // If you need to customise substitution you can work with FontRepository/FontSettings instead.

            // Set a higher resolution for better image quality
            converter.Resolution = new Resolution(300);

            // Prepare the converter
            converter.DoConvert();

            int pageNumber = 1;
            // Iterate through all pages and save each as a BMP image
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.bmp");
                // Save the current page as BMP – use System.Drawing.Imaging.ImageFormat
                converter.GetNextImage(outputPath, ImageFormat.Bmp);
                pageNumber++;
            }
        }

        Console.WriteLine("PDF has been converted to BMP images successfully.");
    }
}
