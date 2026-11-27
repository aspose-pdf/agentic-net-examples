using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Resolution struct lives here

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "Pages";
        const string outputTiff = "output.tiff"; // multi‑page TIFF containing all pages

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // PdfConverter implements IDisposable – wrap it in a using block
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF document
            converter.BindPdf(inputPdf);

            // Set the page range (convert all pages)
            converter.StartPage = 1;
            converter.EndPage   = converter.PageCount; // 1‑based page count

            // Set the desired resolution (dots per inch)
            converter.Resolution = new Resolution(300);

            // Prepare the conversion
            converter.DoConvert();

            // Build the full output path (inside the output folder)
            string outputPath = Path.Combine(outputFolder, outputTiff);

            // Save all pages as a single high‑quality TIFF image (lossless)
            converter.SaveAsTIFF(outputPath);
            Console.WriteLine($"All pages have been saved to TIFF: '{outputPath}'.");
        }

        Console.WriteLine("Conversion completed successfully.");
    }
}
