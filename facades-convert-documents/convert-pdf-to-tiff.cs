using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        try
        {
            PdfConverter converter = new PdfConverter();
            converter.BindPdf(inputPath);
            converter.DoConvert();
            converter.SaveAsTIFF(outputPath);
            Console.WriteLine($"PDF converted to TIFF: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}