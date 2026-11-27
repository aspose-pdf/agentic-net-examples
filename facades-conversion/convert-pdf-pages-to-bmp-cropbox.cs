using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "BmpPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfConverter implements IDisposable – wrap it in a using block
        using (var converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdf);

            // Convert only pages 1 and 2
            converter.StartPage = 1;
            converter.EndPage   = 2;

            // Set the resolution (dpi) for the rasterized images
            converter.Resolution = new Resolution(150);

            // NOTE: PdfConverter does not expose an ImageSaveOptions property.
            // The CropBox is respected by default during conversion, so no extra
            // configuration is required here.

            // Prepare the conversion
            converter.DoConvert();

            int pageNumber = converter.StartPage;
            while (converter.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.bmp");
                // Export the current page as BMP (CropBox is applied automatically)
                converter.GetNextImage(outPath, ImageFormat.Bmp);
                Console.WriteLine($"Saved page {pageNumber} as BMP to '{outPath}'.");
                pageNumber++;
            }

            // Release resources held by the converter
            converter.Close();
        }
    }
}
