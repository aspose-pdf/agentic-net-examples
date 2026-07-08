using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Initialize the PdfConverter facade
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the source PDF file
                converter.BindPdf(inputPdf);

                // Set resolution to 200 DPI
                converter.Resolution = new Resolution(200);

                // NOTE: The CoordinateType enum is no longer part of recent Aspose.Pdf versions.
                // The default coordinate system (CropBox) is used automatically, so the line that
                // attempted to set it has been removed.

                // Prepare the conversion process
                converter.DoConvert();

                int pageNumber = 1;
                // Extract each page as a BMP image
                while (converter.HasNextImage())
                {
                    string outPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");
                    converter.GetNextImage(outPath, ImageFormat.Bmp);
                    pageNumber++;
                }
            }

            Console.WriteLine("PDF successfully converted to BMP images.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
