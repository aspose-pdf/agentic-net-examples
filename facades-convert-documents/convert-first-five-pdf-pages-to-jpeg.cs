using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "output_images";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfConverter facade to convert PDF pages to JPEG images
        using (PdfConverter converter = new PdfConverter())
        {
            // Load the PDF document
            converter.BindPdf(inputPdfPath);

            // Limit conversion to the first five pages
            converter.StartPage = 1;
            converter.EndPage   = 5;

            // Set desired resolution (200 DPI) – requires a Resolution object
            converter.Resolution = new Resolution(200);

            // Prepare the conversion process
            converter.DoConvert();

            int pageIndex = 1;
            while (converter.HasNextImage())
            {
                // Build output file name for each page (extension determines format)
                string outputPath = Path.Combine(outputFolder, $"page_{pageIndex}.jpg");

                // Save the current page as JPEG; format is inferred from the .jpg extension
                converter.GetNextImage(outputPath);

                pageIndex++;
            }
        }

        Console.WriteLine("PDF to JPEG conversion completed.");
    }
}
