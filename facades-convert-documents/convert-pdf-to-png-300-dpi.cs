using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "output_images";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Initialize the PDF converter facade
        Aspose.Pdf.Facades.PdfConverter converter = new Aspose.Pdf.Facades.PdfConverter();

        // Bind the PDF document to the converter
        converter.BindPdf(inputPdfPath);

        // Set the desired resolution (300 DPI)
        converter.Resolution = new Aspose.Pdf.Devices.Resolution(300);

        // Use default coordinate type (CropBox) – no explicit change needed

        // Prepare the converter for image extraction
        converter.DoConvert();

        int pageIndex = 1;
        while (converter.HasNextImage())
        {
            // Build the output file path for each page
            string outputPath = Path.Combine(outputFolder, $"page_{pageIndex}.png");

            // Extract the next page as a PNG image
            converter.GetNextImage(outputPath, ImageFormat.Png);

            pageIndex++;
        }

        // Release resources
        converter.Close();

        Console.WriteLine("PDF successfully converted to PNG images.");
    }
}