using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "BmpPages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfConverter (a Facade) to convert each PDF page to BMP
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdfPath);

            // Prepare the converter (loads pages, etc.)
            converter.DoConvert();

            int pageIndex = 1;
            // Iterate through all pages and save as BMP
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageIndex}.bmp");
                // Save current page image as BMP
                converter.GetNextImage(outputPath, ImageFormat.Bmp);
                pageIndex++;
            }
        }

        Console.WriteLine("PDF to BMP conversion completed.");
    }
}