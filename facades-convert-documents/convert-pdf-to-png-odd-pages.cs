using System;
using System.IO;
using System.Drawing.Imaging;               // ImageFormat for PNG
using Aspose.Pdf.Facades;                  // PdfConverter resides here

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";               // source PDF
        const string outputDir = "OddPageImages";           // folder for PNGs

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfConverter (Facade) to render pages to images
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF file
            converter.BindPdf(inputPdf);

            // Prepare the converter (loads internal structures)
            converter.DoConvert();

            int pageNumber = 1; // 1‑based page index as used by Aspose.Pdf

            // Iterate over all pages
            while (converter.HasNextImage())
            {
                if (pageNumber % 2 == 1) // odd‑numbered page
                {
                    string outPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
                    // Save the current page as PNG
                    converter.GetNextImage(outPath, ImageFormat.Png);
                }
                else
                {
                    // Discard even pages by writing to a temporary stream
                    using (MemoryStream _ = new MemoryStream())
                    {
                        converter.GetNextImage(_, ImageFormat.Png);
                    }
                }

                pageNumber++;
            }
        }

        Console.WriteLine("Odd‑page PNG conversion completed.");
    }
}