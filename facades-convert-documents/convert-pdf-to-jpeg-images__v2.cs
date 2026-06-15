using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "output_images";

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
                // Bind the source PDF file
                converter.BindPdf(inputPdfPath);

                // Prepare the converter (default settings are used)
                converter.DoConvert();

                int pageNumber = 1;
                // GetNextImage() without specifying format saves as JPEG by default
                while (converter.HasNextImage())
                {
                    string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.jpg");
                    converter.GetNextImage(outputPath);
                    pageNumber++;
                }

                // Close the converter and release resources
                converter.Close();
            }

            Console.WriteLine($"PDF pages have been converted to JPEG images in '{outputFolder}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}