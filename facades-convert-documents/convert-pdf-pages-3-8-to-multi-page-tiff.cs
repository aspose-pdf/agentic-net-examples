using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputTiff = "pages3to8.tiff";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfConverter is a Facade that can convert PDF pages to images, including TIFF.
        // It uses the default resolution (150 DPI) and CropBox coordinates by default.
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file.
            converter.BindPdf(inputPdf);

            // Specify the page range to convert (pages are 1‑based).
            converter.StartPage = 3;
            converter.EndPage   = 8;

            // Prepare the converter for processing.
            converter.DoConvert();

            // Save the selected pages as a single multi‑page TIFF file.
            converter.SaveAsTIFF(outputTiff);
        }

        Console.WriteLine($"Pages 3‑8 have been saved to TIFF file: {outputTiff}");
    }
}