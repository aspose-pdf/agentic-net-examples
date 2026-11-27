using System;
using System.IO;
using System.Drawing.Imaging;               // ImageFormat for PNG output
using Aspose.Pdf.Facades;               // PdfConverter resides here

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "Pages_1_to_3";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfConverter implements IDisposable – wrap in using for deterministic cleanup
        using (PdfConverter converter = new PdfConverter())
        {
            // Load the PDF document
            converter.BindPdf(inputPdf);

            // Set the page range (pages 1‑3). StartPage defaults to 1, but we set it explicitly for clarity.
            converter.StartPage = 1;
            converter.EndPage   = 3;

            // Prepare the conversion. No arguments are required.
            converter.DoConvert();

            // Iterate over the selected page range and save each page as a PNG image.
            int pageNumber = converter.StartPage;
            while (converter.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.png");
                converter.GetNextImage(outPath, ImageFormat.Png);
                Console.WriteLine($"Saved page {pageNumber} as PNG to '{outPath}'.");
                pageNumber++;
            }
        }

        Console.WriteLine("Conversion completed.");
    }
}
