using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

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

        // Load the PDF document with deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Initialize the PdfConverter facade
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the loaded document to the converter
                converter.BindPdf(pdfDoc);

                // Set conversion resolution to 600 DPI for high‑quality graphics
                converter.Resolution = new Resolution(600);

                // Prepare the converter for processing
                converter.DoConvert();

                // Convert all pages and save as a single multi‑page TIFF file
                converter.SaveAsTIFF(outputPath);

                // Optional explicit close (Dispose will also handle it)
                converter.Close();
            }
        }

        Console.WriteLine($"PDF successfully converted to TIFF: {outputPath}");
    }
}