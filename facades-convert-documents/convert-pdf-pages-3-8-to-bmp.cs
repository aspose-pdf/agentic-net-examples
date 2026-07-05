using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "BmpImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfConverter (Facade) to convert pages to BMP images
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdfPath);

            // Set the page range (3 to 8, inclusive)
            converter.StartPage = 3;
            converter.EndPage   = 8;

            // Prepare the converter
            converter.DoConvert();

            int currentPage = converter.StartPage;
            // Iterate through the generated images
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"page_{currentPage}.bmp");
                // Save the current page as BMP
                converter.GetNextImage(outputPath, ImageFormat.Bmp);
                currentPage++;
            }
        }

        Console.WriteLine("PDF pages 3‑8 have been converted to BMP images.");
    }
}