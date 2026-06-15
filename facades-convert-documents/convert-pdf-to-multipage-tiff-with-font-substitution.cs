using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.tiff";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create PdfConverter, bind the PDF, configure font substitution, and save as TIFF
        using (PdfConverter converter = new PdfConverter())
        {
            // Load the PDF file
            converter.BindPdf(inputPath);

            // Set rendering options to convert Symbol font to Unicode TTF (Arial Unicode MS)
            converter.RenderingOptions = new RenderingOptions
            {
                ConvertFontsToUnicodeTTF = true
            };

            // Prepare the converter
            converter.DoConvert();

            // Save all pages to a single multi‑page TIFF file
            converter.SaveAsTIFF(outputPath);
        }

        Console.WriteLine($"TIFF image saved to '{outputPath}'.");
    }
}