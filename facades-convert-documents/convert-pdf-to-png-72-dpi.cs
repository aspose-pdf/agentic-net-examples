using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const string outputDir = "output_images";    // folder for PNGs

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // PdfConverter implements IDisposable – use using for deterministic cleanup
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the PDF document to the converter
                converter.BindPdf(inputPdf);

                // Set low resolution (72 DPI) for quick preview images
                converter.Resolution = new Resolution(72);

                // Prepare internal structures for conversion
                converter.DoConvert();

                int pageNumber = 1;
                // Iterate over all pages and save each as PNG
                while (converter.HasNextImage())
                {
                    string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
                    // Format is inferred from the .png extension – no System.Drawing needed
                    converter.GetNextImage(outputPath);
                    pageNumber++;
                }
            }

            Console.WriteLine("PDF to PNG conversion completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
