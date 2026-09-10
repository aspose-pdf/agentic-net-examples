using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;   // ImageFormat for BMP

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputDir = "BmpPages";           // folder for BMP images

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfConverter is a Facade that converts PDF pages to images.
        // It implements IDisposable, so wrap it in a using block.
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdf);

            // Set the page range (Aspose.Pdf uses 1‑based indexing)
            converter.StartPage = 2;   // inclusive start page
            converter.EndPage   = 6;   // inclusive end page

            // Prepare the converter (required before retrieving images)
            converter.DoConvert();

            int pageNumber = converter.StartPage; // will be incremented by the loop
            while (converter.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"page{pageNumber}_out.bmp");

                // Save the current page as BMP.
                // GetNextImage(string, ImageFormat) uses the specified format.
                converter.GetNextImage(outPath, ImageFormat.Bmp);

                Console.WriteLine($"Saved page {pageNumber} → {outPath}");
                pageNumber++;
            }
        }

        Console.WriteLine("PDF to BMP conversion completed.");
    }
}