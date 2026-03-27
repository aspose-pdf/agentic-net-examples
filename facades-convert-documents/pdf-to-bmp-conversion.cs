using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPattern = "page_{0}_out.bmp";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPath);

            // Configure resolution (e.g., 300 DPI)
            converter.Resolution = new Resolution(300);

            // Set the page coordinate type (CropBox is default, MediaBox also possible)
            converter.CoordinateType = PageCoordinateType.CropBox;

            // Perform initial conversion setup
            converter.DoConvert();

            int pageNumber = 1;
            while (converter.HasNextImage())
            {
                string outputFile = string.Format(outputPattern, pageNumber);
                // Save the next page as BMP image
                converter.GetNextImage(outputFile, ImageFormat.Bmp);
                pageNumber++;
            }

            // Optional: explicitly close the converter (Dispose will also be called by using)
            converter.Close();
        }

        Console.WriteLine("PDF to BMP conversion completed.");
    }
}