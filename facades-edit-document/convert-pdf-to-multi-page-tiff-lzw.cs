using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for CompressionType and Resolution

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTiffPath = "output.tiff";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Initialize the PdfConverter which supports TIFF output
                using (PdfConverter converter = new PdfConverter())
                {
                    // Optional: set DPI resolution for the TIFF images
                    // converter.Resolution = new Resolution(300);

                    converter.BindPdf(pdfDoc);
                    converter.DoConvert();

                    // Save the whole PDF as a multi‑page TIFF using LZW compression
                    converter.SaveAsTIFF(outputTiffPath, CompressionType.LZW);
                    converter.Close();
                }
            }

            Console.WriteLine($"PDF successfully converted to multi‑page TIFF: {outputTiffPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
