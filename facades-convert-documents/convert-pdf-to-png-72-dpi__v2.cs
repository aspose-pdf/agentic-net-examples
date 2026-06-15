using System;
using System.IO;
using Aspose.Pdf;                     // Document core API
using Aspose.Pdf.Facades;            // Facade API (PdfConverter)
using Aspose.Pdf.Devices;            // Resolution class
using System.Drawing.Imaging;        // ImageFormat enum

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";

        // Directory where PNG images will be saved
        const string outputFolder = "output_images";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // ------------------------------------------------------------
        // Convert each page of the PDF to a PNG image using PdfConverter
        // ------------------------------------------------------------
        // PdfConverter is part of Aspose.Pdf.Facades and works with the
        // CropBox coordinate type by default (no extra setting required).
        // The Resolution property controls the DPI; we set it to 72.
        // ------------------------------------------------------------
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF file to the converter
            converter.BindPdf(inputPdfPath);

            // Set the desired resolution (72 DPI)
            converter.Resolution = new Resolution(72);

            // Prepare the converter for image extraction
            converter.DoConvert();

            int pageIndex = 1;

            // Extract images while pages remain
            while (converter.HasNextImage())
            {
                // Build the output file name for the current page
                string outputPath = Path.Combine(outputFolder, $"page_{pageIndex}.png");

                // Save the current page as PNG
                converter.GetNextImage(outputPath, ImageFormat.Png);

                pageIndex++;
            }
        }

        Console.WriteLine("PDF successfully converted to PNG images.");
    }
}