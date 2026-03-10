using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string pdfPath = "input.pdf";

        // Directory where BMP images will be saved
        const string outputDir = "output_images";

        // Verify that the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Initialize the PdfConverter facade (abstraction layer)
        PdfConverter converter = new PdfConverter();

        // Bind the PDF document to the converter
        converter.BindPdf(pdfPath);

        // Perform initial conversion setup
        converter.DoConvert();

        int pageNumber = 1;

        // Iterate over each page image produced by the converter
        while (converter.HasNextImage())
        {
            // Build the output BMP file path for the current page
            string bmpPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");

            // Save the current page as a BMP image
            converter.GetNextImage(bmpPath, ImageFormat.Bmp);

            pageNumber++;
        }

        // Release resources held by the converter
        converter.Close();

        Console.WriteLine($"PDF has been converted to BMP images in '{outputDir}'.");
    }
}