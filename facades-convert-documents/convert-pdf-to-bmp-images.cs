using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using System.Drawing.Imaging; // ImageFormat for BMP

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "output";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfConverter is a facade that implements IDisposable
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdf);

            // Configure resolution (e.g., 300 DPI)
            converter.Resolution = new Resolution(300);

            // Set the page coordinate type (CropBox is default; MediaBox is also available)
            converter.CoordinateType = Aspose.Pdf.PageCoordinateType.CropBox;

            // Perform initial conversion setup
            converter.DoConvert();

            int pageNumber = 1;
            // Retrieve each page as a BMP image
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");
                converter.GetNextImage(outputPath, ImageFormat.Bmp);
                pageNumber++;
            }

            // Explicitly close the converter (optional, as using will dispose)
            converter.Close();
        }

        Console.WriteLine("PDF has been converted to BMP images successfully.");
    }
}