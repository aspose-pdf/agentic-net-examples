using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // for ImageFormat

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "TiffPages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfConverter (Facade) to convert each PDF page to a separate TIFF file
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document to the converter
            converter.BindPdf(inputPdf);

            // Perform initial conversion setup
            converter.DoConvert();

            int pageIndex = 1; // Pages are 1‑based in Aspose.Pdf
            while (converter.HasNextImage())
            {
                // Build output file name with page index
                string outputPath = Path.Combine(outputDir, $"page_{pageIndex}.tiff");

                // Save the current page as a TIFF image using System.Drawing.Imaging.ImageFormat
                converter.GetNextImage(outputPath, ImageFormat.Tiff);

                pageIndex++;
            }
        }

        Console.WriteLine("All pages have been saved as individual TIFF files.");
    }
}
