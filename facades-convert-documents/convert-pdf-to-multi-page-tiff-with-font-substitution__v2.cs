using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Initialize the PdfConverter facade
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the loaded PDF document to the converter
                converter.BindPdf(pdfDoc);

                // Enable font substitution: convert Symbol font to Unicode TTF (Arial Unicode MS compatible)
                // This is achieved via RenderingOptions.ConvertFontsToUnicodeTTF
                converter.RenderingOptions.ConvertFontsToUnicodeTTF = true;

                // Perform any necessary pre‑conversion initialization
                converter.DoConvert();

                // Convert all pages to a single multi‑page TIFF file
                converter.SaveAsTIFF(outputTiff);
            }
        }

        Console.WriteLine($"PDF has been converted to TIFF: {outputTiff}");
    }
}