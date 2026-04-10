using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputDir = "TiffPages";          // folder for TIFF files

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfConverter (Facade) to render each page as an image
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document
            converter.BindPdf(inputPdf);

            // Prepare for conversion
            converter.DoConvert();

            int pageIndex = 1;
            // Iterate through all pages
            while (converter.HasNextImage())
            {
                // Build output file name with page index
                string outPath = Path.Combine(outputDir, $"page_{pageIndex}.tiff");

                // Save current page as TIFF
                converter.GetNextImage(outPath, ImageFormat.Tiff);

                pageIndex++;
            }

            // Close the converter (Dispose will also call Close)
            converter.Close();
        }

        Console.WriteLine("PDF has been converted to individual TIFF pages.");
    }
}