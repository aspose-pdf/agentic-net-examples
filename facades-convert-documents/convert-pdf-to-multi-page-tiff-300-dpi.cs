using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

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

        try
        {
            // PdfConverter handles the conversion; it implements IDisposable.
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the source PDF file.
                converter.BindPdf(inputPdf);

                // Set resolution to 300 DPI (default coordinate type is CropBox, no change needed).
                converter.Resolution = new Resolution(300);

                // Prepare the converter.
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
