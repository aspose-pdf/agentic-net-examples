using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Directory where JPEG images will be saved
        const string outputDir = "Images";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // -------------------------------------------------
        // Create and configure the PdfConverter facade
        // -------------------------------------------------
        PdfConverter converter = new PdfConverter();

        // Bind the PDF file to the converter
        converter.BindPdf(inputPdf);

        // Perform the initial conversion setup
        converter.DoConvert();

        // Iterate through each page image and save as JPEG
        int pageNumber = 1;
        while (converter.HasNextImage())
        {
            // Build the output file name with a page number suffix
            string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpg");

            // Save the current page image (default format is JPEG)
            converter.GetNextImage(outputPath);

            pageNumber++;
        }

        // Release resources held by the converter
        converter.Close();
    }
}