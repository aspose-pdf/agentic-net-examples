using System;
using System.IO;
using System.Drawing.Imaging;               // ImageFormat enum
using Aspose.Pdf.Facades;                  // PdfConverter

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputDir = "ExportedImages";     // folder for JPEGs

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfConverter (Facade) to extract images
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF file to the converter
            converter.BindPdf(inputPdf);

            // Prepare the converter for image extraction
            converter.DoConvert();

            int imageIndex = 1;
            // Loop through all images in the PDF
            while (converter.HasNextImage())
            {
                // Build the output file name (e.g., image1.jpg, image2.jpg, ...)
                string outputFile = Path.Combine(outputDir, $"image{imageIndex}.jpg");

                // Export the current image as JPEG with quality = 85
                // GetNextImage(string outputFile, ImageFormat format, int quality)
                converter.GetNextImage(outputFile, ImageFormat.Jpeg, 85);

                imageIndex++;
            }
        }

        Console.WriteLine("Image export completed.");
    }
}