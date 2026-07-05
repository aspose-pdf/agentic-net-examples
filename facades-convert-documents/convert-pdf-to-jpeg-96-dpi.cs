using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class PdfToJpegConverter
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Directory where JPEG images will be saved
        const string outputDir = "JpegPages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfConverter (facade) to convert each page to JPEG
        using (PdfConverter converter = new PdfConverter())
        {
            // Set desired resolution (96 DPI for web‑friendly images)
            // PdfConverter.Resolution expects a Resolution object, not an int.
            converter.Resolution = new Resolution(96);

            // Bind the source PDF file
            converter.BindPdf(inputPdf);

            // Prepare the converter for image extraction
            converter.DoConvert();

            int pageNumber = 1;
            // Iterate through all pages and save each as a JPEG file
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpg");
                // GetNextImage overload infers format from file extension (".jpg")
                converter.GetNextImage(outputPath);
                pageNumber++;
            }
        }

        Console.WriteLine("PDF pages have been converted to JPEG images.");
    }
}
