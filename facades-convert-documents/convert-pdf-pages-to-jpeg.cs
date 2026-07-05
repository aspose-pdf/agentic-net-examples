using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Directory where JPEG images will be saved
        const string outputDir = "OutputImages";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Validate input file existence
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfConverter (Facade) to convert each page to a JPEG image
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document to the converter
            converter.BindPdf(inputPdf);

            // Prepare the converter for image extraction
            converter.DoConvert();

            int pageNumber = 1;
            // Loop through all pages; each call to GetNextImage saves the next page as JPEG
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpg");
                converter.GetNextImage(outputPath); // default format is JPEG
                Console.WriteLine($"Saved page {pageNumber} as {outputPath}");
                pageNumber++;
            }
        }

        Console.WriteLine("PDF to JPEG conversion completed.");
    }
}