using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Directory where JPEG images will be saved
        const string outputFolder = "Images";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfConverter (Facade) to convert each page to a JPEG image
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document to the converter
            converter.BindPdf(inputPdfPath);

            // Prepare the converter for processing
            converter.DoConvert();

            int pageIndex = 1;

            // Iterate through all pages and save each as a JPEG image
            while (converter.HasNextImage())
            {
                string outputFile = Path.Combine(outputFolder, $"page_{pageIndex}.jpg");
                // Saves the current page image using the default JPEG format,
                // preserving the original page dimensions and color depth.
                converter.GetNextImage(outputFile);
                pageIndex++;
            }
        }

        Console.WriteLine("PDF to JPEG conversion completed successfully.");
    }
}