using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfConverter
using Aspose.Pdf;                // ImageFormat

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output directory for JPEG images (ensure it exists)
        const string outputDir = "Images";
        Directory.CreateDirectory(outputDir);

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Initialize the converter facade
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the PDF file to the converter
                converter.BindPdf(inputPdf);

                // Prepare conversion (required before retrieving images)
                converter.DoConvert();

                // Sequentially retrieve each page as a JPEG image
                int pageNumber = 1;
                while (converter.HasNextImage())
                {
                    // Build output file name with page number suffix
                    string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpg");

                    // Save the current page image (default format is JPEG)
                    converter.GetNextImage(outputPath);

                    pageNumber++;
                }
            }

            Console.WriteLine("PDF successfully converted to JPEG images.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}