using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF file
        const string outputTiff = "output.tiff"; // destination TIFF file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // PdfConverter is a Facade that handles PDF loading and conversion.
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the PDF document to the converter.
                converter.BindPdf(inputPdf);

                // Prepare the converter for image extraction.
                converter.DoConvert();

                // Convert all pages to a single multi‑page TIFF file.
                converter.SaveAsTIFF(outputTiff);
            }

            Console.WriteLine($"PDF successfully converted to TIFF: {outputTiff}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}