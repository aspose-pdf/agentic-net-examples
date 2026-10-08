using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF
        const string outputDir = "TiffPages"; // folder for TIFF images
        const int dpi = 400;                    // desired resolution

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfConverter implements IDisposable – use a using block for deterministic cleanup
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document to the converter
            converter.BindPdf(inputPdf);

            // Set the resolution (DPI) for the output images using a Resolution object
            converter.Resolution = new Resolution(dpi);

            // Define the page range to convert (all pages)
            converter.StartPage = 1;
            converter.EndPage   = converter.PageCount; // available after BindPdf

            // Prepare the conversion
            converter.DoConvert();

            // Loop through each page and save as a separate TIFF image
            for (int page = converter.StartPage; page <= converter.EndPage; page++)
            {
                string outPath = Path.Combine(outputDir, $"page_{page}.tiff");
                // GetNextImage infers the format from the file extension (TIFF here)
                converter.GetNextImage(outPath);
                Console.WriteLine($"Saved page {page} as TIFF → {outPath}");
            }
        }

        Console.WriteLine("PDF to TIFF conversion completed.");
    }
}
