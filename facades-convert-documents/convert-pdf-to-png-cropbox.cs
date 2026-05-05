using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";

        // Directory where PNG images will be saved
        const string outputDir = "output_images";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Initialize the PdfConverter facade
        PdfConverter converter = new PdfConverter();

        // Bind the PDF file to the converter
        converter.BindPdf(inputPdfPath);

        // Use the CropBox coordinate type so only the visible page area is rendered
        converter.CoordinateType = PageCoordinateType.CropBox;

        // Prepare the converter for image extraction
        converter.DoConvert();

        int pageIndex = 1;
        // Extract each page as a PNG image
        while (converter.HasNextImage())
        {
            string outputPath = Path.Combine(outputDir, $"page_{pageIndex}.png");
            converter.GetNextImage(outputPath, ImageFormat.Png);
            pageIndex++;
        }

        // Release resources held by the converter
        converter.Close();

        Console.WriteLine($"Conversion complete. PNG files are saved in '{outputDir}'.");
    }
}