using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Added for Resolution class

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

        // Initialize the PDF converter facade
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdf);

            // Set the desired resolution (400 DPI) for high‑resolution archival output
            converter.Resolution = new Resolution(400);

            // Prepare the conversion process
            converter.DoConvert();

            // Convert all pages to a single multi‑page TIFF file
            converter.SaveAsTIFF(outputTiff);
        }

        Console.WriteLine($"Conversion completed: '{outputTiff}'");
    }
}
