using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Convert PDF to TIFF with Symbol‑to‑Arial Unicode MS substitution
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF
            converter.BindPdf(inputPdf);

            // Enable font conversion to Unicode TrueType (Arial Unicode MS compatible)
            converter.RenderingOptions = new RenderingOptions
            {
                ConvertFontsToUnicodeTTF = true
            };

            // Prepare the converter
            converter.DoConvert();

            // Save all pages as a single TIFF file
            converter.SaveAsTIFF(outputTiff);
        }

        Console.WriteLine($"TIFF image saved to '{outputTiff}'.");
    }
}