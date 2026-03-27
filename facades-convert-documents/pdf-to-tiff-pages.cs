using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfConverter converter = new PdfConverter())
        {
            converter.BindPdf(inputPath);
            converter.DoConvert();

            int pageIndex = 1;
            while (converter.HasNextImage())
            {
                string outputFile = $"page_{pageIndex}.tiff";
                converter.GetNextImage(outputFile, ImageFormat.Tiff);
                pageIndex++;
            }

            converter.Close();
        }

        Console.WriteLine("PDF pages have been converted to separate TIFF files.");
    }
}