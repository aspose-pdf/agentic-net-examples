using System;
using System.IO;
using Aspose.Pdf.Facades;

class PdfToJpegConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output folder for JPEG images
        const string outputFolder = "output_images";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // PdfConverter is a Facade that converts PDF pages to images
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the PDF document to the converter
                converter.BindPdf(inputPdfPath);

                // Prepare the converter for image extraction
                converter.DoConvert();

                int pageNumber = 1;

                // Iterate through all pages and save each as a JPEG image
                while (converter.HasNextImage())
                {
                    string outputImagePath = Path.Combine(outputFolder, $"page_{pageNumber}.jpg");

                    // GetNextImage(string) saves the current page as JPEG by default
                    converter.GetNextImage(outputImagePath);

                    pageNumber++;
                }
            }

            Console.WriteLine($"PDF pages have been converted to JPEG images in '{outputFolder}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}