using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "TiffPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfConverter (Facade) to convert each page to a separate TIFF image
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdfPath);

            // Prepare the converter (required before extracting images)
            converter.DoConvert();

            int pageIndex = 1;
            while (converter.HasNextImage())
            {
                // Build the output file name for the current page
                string tiffPath = Path.Combine(outputFolder, $"page_{pageIndex}.tiff");

                // Save the current page as a TIFF image using default settings
                // Use System.Drawing.Imaging.ImageFormat for the format specification
                converter.GetNextImage(tiffPath, ImageFormat.Tiff);

                pageIndex++;
            }
        }

        Console.WriteLine("PDF has been converted to individual TIFF images.");
    }
}
