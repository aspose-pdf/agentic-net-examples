using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // required for Resolution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Images";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfConverter implements IDisposable
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document to the converter
            converter.BindPdf(inputPdf);

            // Set the desired resolution (300 DPI)
            // Resolution property expects an Aspose.Pdf.Devices.Resolution instance
            converter.Resolution = new Resolution(300);

            // Use CropBox coordinates for precise cropping
            // The enum PageCoordinateType is defined in Aspose.Pdf namespace (not nested inside PdfConverter)
            converter.CoordinateType = PageCoordinateType.CropBox;

            // Initialize conversion process
            converter.DoConvert();

            int pageNumber = 1;
            // Iterate through all pages, saving each as a JPEG image
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpg");
                // GetNextImage(string) saves the next page as JPEG by default
                converter.GetNextImage(outputPath);
                pageNumber++;
            }
        }

        Console.WriteLine("PDF to JPEG conversion completed.");
    }
}
