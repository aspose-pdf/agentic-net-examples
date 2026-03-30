using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.tiff";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        PdfConverter converter = new PdfConverter();
        converter.BindPdf(inputPath);
        converter.DoConvert();
        converter.SaveAsTIFF(outputPath, CompressionType.LZW);
        converter.Close();

        Console.WriteLine($"PDF successfully converted to multi-page TIFF with LZW compression: {outputPath}");
    }
}