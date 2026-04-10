using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const string outputDir = "Images";            // folder for JPEGs

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfConverter is a Facade class – use it for PDF‑to‑image conversion
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document
            converter.BindPdf(inputPdf);

            // Set resolution to 150 dpi (suitable for web)
            converter.Resolution = new Resolution(150);

            // Prepare the converter for image extraction
            converter.DoConvert();

            int pageNumber = 1;
            // Iterate over all pages and save each as a JPEG
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpg");

                // GetNextImage(string) infers the image format from the file extension.
                converter.GetNextImage(outputPath);

                pageNumber++;
            }

            // Explicitly close the converter (optional, as using will dispose)
            converter.Close();
        }

        Console.WriteLine("PDF successfully converted to JPEG images.");
    }
}
