using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class PdfToBmpConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output directory for BMP images
        const string outputDir = "BmpImages";

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create the output directory if it does not exist
        Directory.CreateDirectory(outputDir);

        // Use PdfConverter from Aspose.Pdf.Facades to convert pages to BMP
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF file to the converter
            converter.BindPdf(inputPdf);

            // Specify the page range (pages are 1‑based)
            converter.StartPage = 2; // start page
            converter.EndPage   = 6; // end page

            // Prepare the converter for image extraction
            converter.DoConvert();

            int pageIndex = converter.StartPage; // will be 2
            while (converter.HasNextImage())
            {
                // Build the output file name for each page
                string outputPath = Path.Combine(outputDir, $"page_{pageIndex}.bmp");

                // Save the current page as BMP
                converter.GetNextImage(outputPath, ImageFormat.Bmp);

                pageIndex++;
            }
        }

        Console.WriteLine("BMP conversion completed.");
    }
}