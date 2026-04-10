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
        const string outputFolder = "output_images";

        // Validate input file
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfConverter (facade) to convert each page to JPEG
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document
            converter.BindPdf(inputPdf);

            // Prepare the converter for image extraction
            converter.DoConvert();

            int pageNumber = 1;
            // Iterate through all pages and save as JPEG files
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.jpg");
                // GetNextImage saves the current page image using the default JPEG format
                converter.GetNextImage(outputPath);
                pageNumber++;
            }
        }

        Console.WriteLine("PDF conversion to JPEG images completed.");
    }
}