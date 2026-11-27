using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "TiffPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Initialize PdfConverter facade
        using (PdfConverter converter = new PdfConverter())
        {
            // Load and bind the PDF document
            converter.BindPdf(inputPdf);

            // Set resolution to 120 DPI (Aspose.Pdf.Devices.Resolution)
            converter.Resolution = new Resolution(120);

            // Convert pages 1‑4 to separate TIFF files
            for (int page = 1; page <= 4; page++)
            {
                converter.StartPage = page;
                converter.EndPage   = page;

                // Perform the conversion for the selected page range
                converter.DoConvert();

                string outPath = Path.Combine(outputDir, $"Page_{page}.tiff");
                // Save the converted page as a TIFF image
                converter.SaveAsTIFF(outPath);
            }
        }

        Console.WriteLine("Pages 1‑4 have been converted to TIFF images.");
    }
}
