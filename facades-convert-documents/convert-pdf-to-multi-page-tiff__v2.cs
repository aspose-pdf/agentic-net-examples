using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // For Resolution and TiffSettings if needed

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputTiff = "output.tiff";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfConverter facade to convert PDF pages to a single TIFF image
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdf);

            // Set rendering options to substitute fonts (e.g., Symbol → Arial Unicode MS)
            // ConvertFontsToUnicodeTTF forces fonts to be converted to Unicode TrueType fonts,
            // which provides better compatibility for symbols.
            converter.RenderingOptions = new RenderingOptions
            {
                ConvertFontsToUnicodeTTF = true
            };

            // Optional: set a higher resolution for better image quality
            converter.Resolution = new Resolution(300);

            // Prepare the converter (required before saving)
            converter.DoConvert();

            // Save all pages as a single multi‑page TIFF file
            converter.SaveAsTIFF(outputTiff);
        }

        Console.WriteLine($"PDF has been converted to TIFF: {outputTiff}");
    }
}