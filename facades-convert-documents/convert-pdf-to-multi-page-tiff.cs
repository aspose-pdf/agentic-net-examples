using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Resolution class

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputTiff = "output.tiff";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document (ensures proper disposal)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create the PdfConverter facade
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the loaded document to the converter
                converter.BindPdf(pdfDoc);

                // Set high‑quality resolution (300 DPI)
                converter.Resolution = new Resolution(300);

                // Prepare the converter for processing
                converter.DoConvert();

                // Convert all pages to a single multi‑page TIFF file
                converter.SaveAsTIFF(outputTiff);

                // Release converter resources
                converter.Close();
            }
        }

        Console.WriteLine($"PDF successfully converted to TIFF: {outputTiff}");
    }
}