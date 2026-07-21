using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Create and configure the PdfConverter facade
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdf);

            // NOTE: The CoordinateType property does not exist in recent Aspose.Pdf versions.
            // CropBox is used by default, so we simply omit any explicit setting.

            // Prepare the converter for image extraction
            converter.DoConvert();

            int pageNumber = 1;
            // Extract each page as a PNG image
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");
                converter.GetNextImage(outputPath, ImageFormat.Png);
                pageNumber++;
            }
        }

        Console.WriteLine("PDF has been converted to PNG images successfully.");
    }
}
