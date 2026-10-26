using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTiff = "output.tiff";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Register font substitution: replace Symbol with Arial Unicode MS
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Symbol", "Arial Unicode MS"));

        // Initialize the PdfConverter facade
        PdfConverter converter = new PdfConverter();

        // Load the PDF document
        converter.BindPdf(inputPdf);

        // Convert all pages (1‑based indexing)
        converter.StartPage = 1;
        converter.EndPage = converter.PageCount;

        // Save as a multi‑page TIFF at 300 DPI using CCITT Group4 compression
        const int resolution = 300;
        // SaveAsTIFF expects an int for compression, so cast the enum value
        converter.SaveAsTIFF(outputTiff, resolution, (int)CompressionType.CCITT4);

        Console.WriteLine($"PDF successfully converted to TIFF: {outputTiff}");
    }
}
