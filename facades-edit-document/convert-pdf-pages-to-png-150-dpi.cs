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
        const string outputFolder = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfConverter within a using block for proper disposal
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdf);

            // Set the desired resolution (150 DPI)
            converter.Resolution = new Resolution(150);

            // NOTE: The ImageTransparent property is not available in the current
            // Aspose.Pdf.Facades version. Transparent background for PNG output
            // can be achieved via the ImageDevice API if required. For the purpose
            // of this example we proceed without setting it.

            // Initialize the conversion process
            converter.DoConvert();

            int pageNumber = 1;
            // Iterate through all pages and save each as a PNG image
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");
                converter.GetNextImage(outputPath, ImageFormat.Png);
                pageNumber++;
            }
        }

        Console.WriteLine("All pages have been converted to PNG images at 150 DPI.");
    }
}
