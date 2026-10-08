using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "TiffPages";
        const string outputTiff = "output.tiff"; // multi‑page TIFF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);
        string outPath = Path.Combine(outputDir, outputTiff);

        // PdfConverter implements IDisposable, so wrap it in a using block.
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF.
            converter.BindPdf(inputPdf);

            // Set the page range – from first to last page.
            converter.StartPage = 1;
            converter.EndPage   = converter.PageCount; // total pages in the document

            // Set the desired resolution (300 DPI). The default coordinate type is used automatically.
            converter.Resolution = new Resolution(300);

            // Perform the conversion.
            converter.DoConvert();

            // Save all pages as a single multi‑page TIFF image.
            converter.SaveAsTIFF(outPath);
        }

        Console.WriteLine($"PDF has been converted to TIFF: {outPath}");
        Console.WriteLine("PDF to TIFF conversion completed.");
    }
}
