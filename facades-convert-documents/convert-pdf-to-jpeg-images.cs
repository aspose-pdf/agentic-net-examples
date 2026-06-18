using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "Images";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfConverter (Facade) to convert each page to JPEG
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdfPath);

            // Prepare the converter (required before extracting images)
            converter.DoConvert();

            int pageIndex = 1;
            // Iterate through all pages and save each as a JPEG image
            while (converter.HasNextImage())
            {
                string outputFile = Path.Combine(outputFolder, $"page_{pageIndex}.jpg");
                // GetNextImage saves the current page as JPEG using original page dimensions
                converter.GetNextImage(outputFile);
                pageIndex++;
            }
        }

        Console.WriteLine("PDF successfully converted to JPEG images.");
    }
}