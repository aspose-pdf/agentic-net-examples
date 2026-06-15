using System;
using System.IO;
using System.Drawing.Imaging;          // ImageFormat for BMP
using Aspose.Pdf.Facades;              // PdfConverter facade

class PdfToBmpConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Directory where BMP images will be saved
        const string outputDir = "BmpPages";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Validate input file existence
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfConverter (facade) to convert pages to BMP images
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdf);

            // Specify the page range (2 to 6, inclusive)
            converter.StartPage = 2;   // minimal value is 1
            converter.EndPage   = 6;

            // Prepare the converter for image extraction
            converter.DoConvert();

            // Counter to generate sequential file names matching the actual page numbers
            int currentPage = converter.StartPage;

            // Extract each page as a BMP image
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"page_{currentPage}.bmp");

                // Save the current page image in BMP format
                converter.GetNextImage(outputPath, ImageFormat.Bmp);

                Console.WriteLine($"Saved page {currentPage} as BMP -> {outputPath}");
                currentPage++;
            }
        }

        Console.WriteLine("PDF to BMP conversion completed.");
    }
}