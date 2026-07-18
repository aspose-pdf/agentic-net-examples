using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class PdfToBmpConverter
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "BmpPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Open the PDF document (required for binding)
            using (Document pdfDoc = new Document(inputPdf))
            // Initialize the PdfConverter facade
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the opened document to the converter
                converter.BindPdf(pdfDoc);
                // Set a high resolution (e.g., 300 DPI) using the correct Resolution type
                converter.Resolution = new Resolution(300);
                // Prepare the converter for conversion
                converter.DoConvert();

                int pageIndex = 1;
                // Iterate over all pages and save each as a BMP image
                while (converter.HasNextImage())
                {
                    string outputPath = Path.Combine(outputDir, $"page_{pageIndex}.bmp");
                    // Save the current page; format is inferred from the .bmp extension
                    converter.GetNextImage(outputPath);
                    pageIndex++;
                }
            }

            Console.WriteLine("PDF pages have been converted to BMP images successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
