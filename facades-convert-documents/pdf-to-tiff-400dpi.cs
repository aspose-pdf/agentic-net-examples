using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.tiff";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfConverter implements IDisposable, so use a using block
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF document
            converter.BindPdf(inputPath);

            // Set the desired resolution (DPI) for the output images
            converter.Resolution = new Resolution(400);

            // Perform the conversion
            converter.DoConvert();

            // Save all pages as a single TIFF file
            converter.SaveAsTIFF(outputPath);
        }

        Console.WriteLine($"TIFF image saved to '{outputPath}' with 400 DPI resolution.");
    }
}
