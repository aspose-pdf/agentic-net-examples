using System;
using System.IO;
using System.Drawing.Imaging;          // ImageFormat enum
using Aspose.Pdf.Facades;            // PdfConverter and related enums

class PdfToBmpConverter
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Directory where BMP images will be saved
        const string outputDir = "BmpImages";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Validate input file existence
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfConverter (Facade) to convert each page to BMP.
        // The converter implements IDisposable, so wrap it in a using block.
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document to the converter.
            converter.BindPdf(inputPdf);

            // NOTE: The CoordinateType property and PageCoordinateType enum are no longer required.
            // CropBox is used by default for margin cropping, so we omit setting it explicitly.

            // Prepare the converter for image extraction.
            converter.DoConvert();

            int pageIndex = 1;
            // Iterate over all pages and save each as a BMP image.
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageIndex}.bmp");

                // Save the current page image as BMP.
                // GetNextImage(string, ImageFormat) writes the image to the specified file.
                converter.GetNextImage(outputPath, ImageFormat.Bmp);

                Console.WriteLine($"Saved page {pageIndex} as BMP -> {outputPath}");
                pageIndex++;
            }
        }

        Console.WriteLine("PDF to BMP conversion completed.");
    }
}
