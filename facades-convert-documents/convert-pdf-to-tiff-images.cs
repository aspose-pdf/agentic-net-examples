using System;
using System.IO;
using System.Drawing.Imaging; // Added for ImageFormat
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "TiffPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfConverter facade to convert each page to a TIFF image
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdf);

            // Prepare the converter (required before extracting images)
            converter.DoConvert();

            int pageNumber = 1;
            // Extract images page by page; each call saves one TIFF file
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.tiff");
                // Save the current page as a TIFF image using default settings
                converter.GetNextImage(outputPath, ImageFormat.Tiff); // Fixed namespace
                pageNumber++;
            }
        }

        Console.WriteLine("PDF to TIFF conversion completed.");
    }
}
