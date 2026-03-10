using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // required for Resolution
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Images";

        try
        {
            if (!File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"File not found: {inputPdf}");
                return;
            }

            Directory.CreateDirectory(outputDir);

            // Convert PDF pages to high‑quality JPEG images using the PdfConverter facade
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the source PDF file
                converter.BindPdf(inputPdf);

                // Set a high resolution (e.g., 300 DPI) for clearer images
                converter.Resolution = new Resolution(300);

                // NOTE: PdfConverter does not expose a JPEG quality property.
                // The image quality is primarily controlled by the resolution.
                // If explicit JPEG quality control is required, switch to JpegDevice.

                // Prepare the converter
                converter.DoConvert();

                int pageNumber = 1;
                while (converter.HasNextImage())
                {
                    string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpg");
                    // Save the current page as JPEG (quality governed by resolution)
                    converter.GetNextImage(outputPath, ImageFormat.Jpeg);
                    pageNumber++;
                }
            }

            Console.WriteLine("PDF has been successfully converted to JPEG images.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
