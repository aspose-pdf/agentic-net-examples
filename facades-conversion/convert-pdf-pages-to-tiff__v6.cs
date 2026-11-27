using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // ImageFormat for specifying TIFF

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Directory where TIFF images will be saved
        const string outputDir = "TiffPages";

        // Validate input file
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Create the PdfConverter (facade API)
        PdfConverter converter = new PdfConverter();

        // Bind the PDF document to the converter
        converter.BindPdf(inputPdf);

        // Start the conversion process
        converter.DoConvert();

        int pageNumber = 1;

        // Loop through all pages that contain images
        while (converter.HasNextImage())
        {
            // Build a unique file name for each page
            string tiffPath = Path.Combine(outputDir, $"Page_{pageNumber}.tiff");

            // Save the current page as a TIFF image
            // GetNextImage requires a destination path and an ImageFormat
            converter.GetNextImage(tiffPath, ImageFormat.Tiff);

            pageNumber++;
        }

        // Release any resources held by the converter
        converter.Close();

        Console.WriteLine($"Conversion completed. TIFF images saved to '{outputDir}'.");
    }
}