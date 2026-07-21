using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // required for Resolution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTiff = "output.tiff";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the PdfConverter facade
            PdfConverter converter = new PdfConverter();

            // Bind the loaded document to the converter
            converter.BindPdf(doc);

            // Set the desired resolution (300 DPI) using a Resolution object
            converter.Resolution = new Resolution(300);

            // Perform any necessary initialization before conversion
            converter.DoConvert();

            // Save all pages as a single multi‑page TIFF file
            converter.SaveAsTIFF(outputTiff);
        }

        Console.WriteLine($"PDF has been converted to TIFF: {outputTiff}");
    }
}
