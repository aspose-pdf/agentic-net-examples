using System;
using System.IO;
using System.Drawing.Imaging; // ImageFormat
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "odd_pages_png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Determine total number of pages (1‑based indexing)
        int totalPages;
        using (Document doc = new Document(inputPdf))
        {
            totalPages = doc.Pages.Count;
        }

        // Process only odd‑numbered pages
        for (int pageNumber = 1; pageNumber <= totalPages; pageNumber += 2)
        {
            // Each conversion uses a fresh PdfConverter instance (IDisposable)
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the source PDF file
                converter.BindPdf(inputPdf);

                // Restrict conversion to the current odd page
                converter.StartPage = pageNumber;
                converter.EndPage   = pageNumber;

                // Prepare the converter
                converter.DoConvert();

                // Build output file name (e.g., page_1.png, page_3.png, ...)
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");

                // Save the page as PNG
                converter.GetNextImage(outputPath, ImageFormat.Png);
            }
        }

        Console.WriteLine($"Odd‑page PNG images saved to '{outputFolder}'.");
    }
}