using System;
using System.IO;
using System.Drawing.Imaging;          // For ImageFormat.Png
using Aspose.Pdf.Facades;             // Facade classes (PdfConverter)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";               // Source PDF
        const string outputDir = "OddPageImages";           // Folder for PNGs

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfConverter (IDisposable) – it will be disposed automatically
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF file to the converter
            converter.BindPdf(inputPdf);

            // Total number of pages (1‑based indexing)
            int pageCount = converter.Document.Pages.Count;

            // Iterate over odd‑numbered pages only
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber += 2)
            {
                // Restrict conversion to a single page
                converter.StartPage = pageNumber;
                converter.EndPage   = pageNumber;

                // Prepare the converter for the specified range
                converter.DoConvert();

                // Build output file name (e.g., page_1.png, page_3.png, …)
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");

                // Save the page as PNG
                converter.GetNextImage(outputPath, ImageFormat.Png);
            }
        }

        Console.WriteLine("Odd‑page PNG conversion completed.");
    }
}