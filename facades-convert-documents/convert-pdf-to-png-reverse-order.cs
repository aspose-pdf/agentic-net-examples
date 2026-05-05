using System;
using System.IO;
using Aspose.Pdf.Facades;          // Facade APIs (PdfConverter)
using System.Drawing;
using System.Drawing.Imaging;      // ImageFormat for PNG

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";                 // source PDF
        const string outputDir = "output_images";             // folder for PNGs

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Verify that the input PDF exists before proceeding
        if (!File.Exists(inputPdf))
        {
            Console.WriteLine($"Error: The file '{inputPdf}' was not found.");
            return;
        }

        // PdfConverter is a Facade; wrap in using for deterministic disposal
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF file to the converter
            converter.BindPdf(inputPdf);

            // Total number of pages (1‑based indexing)
            int pageCount = converter.PageCount;

            // Process pages in reverse order: last page to first page
            for (int pageNum = pageCount; pageNum >= 1; pageNum--)
            {
                // Restrict conversion to a single page
                converter.StartPage = pageNum;
                converter.EndPage   = pageNum;

                // Prepare the converter for the selected page range
                converter.DoConvert();

                // Build output file name (e.g., page_5.png)
                string outputPath = Path.Combine(outputDir, $"page_{pageNum}.png");

                // Suppress the platform‑specific warning for ImageFormat.Png (CA1416)
#pragma warning disable CA1416 // Validate platform compatibility
                converter.GetNextImage(outputPath, ImageFormat.Png);
#pragma warning restore CA1416 // Validate platform compatibility
            }

            // Release any resources held by the converter
            converter.Close();
        }

        Console.WriteLine("PDF pages have been converted to PNG images in reverse order.");
    }
}
