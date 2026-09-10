using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";               // source PDF
        const string outputDir = "PagesAsTiff";             // folder for page images

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfConverter (Facade) to extract each page as a TIFF image
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF file
            converter.BindPdf(inputPdf);

            // Prepare for conversion
            converter.DoConvert();

            int pageIndex = 1;
            // Loop while there are more pages to convert
            while (converter.HasNextImage())
            {
                // Build output file name: e.g., PagesAsTiff/page_1.tiff
                string outPath = Path.Combine(outputDir, $"page_{pageIndex}.tiff");

                // Save current page as TIFF
                converter.GetNextImage(outPath, ImageFormat.Tiff);

                pageIndex++;
            }
        }

        Console.WriteLine("PDF pages have been saved as individual TIFF files.");
    }
}