using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "BmpImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // PdfConverter is a Facade that implements IDisposable
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the PDF file to the converter
                converter.BindPdf(inputPdf);

                // Limit conversion to pages 1 through 10
                converter.StartPage = 1;   // minimal value is 1
                converter.EndPage   = 10;  // end page inclusive

                // Prepare the converter (required before extracting images)
                converter.DoConvert();

                int imageIndex = 1;
                // Loop through available images and save each as BMP
                while (converter.HasNextImage())
                {
                    string outputPath = Path.Combine(outputDir, $"page_{imageIndex}.bmp");
                    converter.GetNextImage(outputPath, ImageFormat.Bmp);
                    imageIndex++;
                }
            }

            Console.WriteLine($"Conversion completed. BMP images saved to '{outputDir}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}