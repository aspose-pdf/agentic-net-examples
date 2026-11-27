using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "TiffPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // PdfConverter does not implement IDisposable, so we manage its lifetime manually.
        PdfConverter converter = new PdfConverter();
        try
        {
            // Bind the source PDF document.
            converter.BindPdf(inputPdf);

            // Define the page range to convert.
            converter.StartPage = 5;
            converter.EndPage   = 7;

            // Optional: set resolution for the rasterized images.
            converter.Resolution = new Resolution(150);

            // Prepare the conversion.
            converter.DoConvert();

            // Convert each page in the range to a TIFF image.
            int page = converter.StartPage;
            while (converter.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"page_{page}.tiff");
                converter.GetNextImage(outPath, ImageFormat.Tiff);
                Console.WriteLine($"Saved page {page} as TIFF → {outPath}");
                page++;
            }
        }
        finally
        {
            // Release any resources held by the converter.
            converter.Close();
        }
    }
}
