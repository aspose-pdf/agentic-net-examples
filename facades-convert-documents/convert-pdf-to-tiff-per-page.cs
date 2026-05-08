using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "TiffPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfConverter facade to extract each page as a TIFF image
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdf);
            // Prepare the converter for image extraction
            converter.DoConvert();

            int pageIndex = 1;
            // Iterate through all pages and save each as a separate TIFF file
            while (converter.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"page_{pageIndex}.tiff");
                // Save the current page image in TIFF format
                converter.GetNextImage(outPath, ImageFormat.Tiff);
                pageIndex++;
            }
        }

        Console.WriteLine("PDF to TIFF conversion completed.");
    }
}