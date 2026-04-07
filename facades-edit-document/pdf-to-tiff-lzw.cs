using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "output.tiff";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        PdfConverter converter = new PdfConverter();
        try
        {
            converter.BindPdf(inputPath);
            converter.DoConvert();
            converter.SaveAsTIFF(outputPath, CompressionType.LZW);
        }
        finally
        {
            converter.Close();
        }

        Console.WriteLine($"PDF converted to multi-page TIFF with LZW compression: {outputPath}");
    }
}
