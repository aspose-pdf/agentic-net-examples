using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "output_images";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfConverter (Facade) to extract images page by page
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF
            converter.BindPdf(inputPdf);

            // Prepare the converter (required before extracting images)
            converter.DoConvert();

            int pageNumber = 1;
            // Extract each page as a JPEG using the default format
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.jpg");
                converter.GetNextImage(outputPath); // default image format is JPEG
                pageNumber++;
            }
        }

        Console.WriteLine("PDF successfully converted to JPEG images.");
    }
}