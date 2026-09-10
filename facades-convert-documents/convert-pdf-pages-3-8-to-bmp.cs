using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // ImageFormat for BMP

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "BmpImages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfConverter (Facade) to convert pages to BMP
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF
            converter.BindPdf(inputPdf);

            // Define the page range (1‑based indexing)
            converter.StartPage = 3;
            converter.EndPage   = 8;

            // Prepare the converter
            converter.DoConvert();

            // Image files will be named page3.bmp, page4.bmp, ...
            int pageNumber = converter.StartPage;
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"page{pageNumber}.bmp");
                converter.GetNextImage(outputPath, ImageFormat.Bmp);
                pageNumber++;
            }
        }

        Console.WriteLine("PDF pages 3‑8 have been converted to BMP images.");
    }
}