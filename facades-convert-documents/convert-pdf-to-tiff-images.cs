using System;
using System.IO;
using System.Drawing.Imaging; // Added for ImageFormat
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "TiffPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Use PdfConverter facade to convert each page to a TIFF image
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdf);

            // Prepare for conversion (default settings are used)
            converter.DoConvert();

            int pageIndex = 1;
            // Loop through all pages and save each as a separate TIFF file
            while (converter.HasNextImage())
            {
                string tiffPath = Path.Combine(outputDir, $"page_{pageIndex}.tiff");
                // Save the current page as TIFF using default resolution and settings
                // Use System.Drawing.Imaging.ImageFormat instead of the removed Aspose enum
                converter.GetNextImage(tiffPath, ImageFormat.Tiff);
                pageIndex++;
            }
        }

        Console.WriteLine("PDF has been converted to individual TIFF images.");
    }
}
