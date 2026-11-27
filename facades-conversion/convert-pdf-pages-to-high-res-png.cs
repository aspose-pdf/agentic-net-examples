using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "Images";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // PdfConverter implements IDisposable – wrap it in a using block
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdfPath);

            // Set the page range to convert (pages 2‑4)
            converter.StartPage = 2;
            converter.EndPage   = 4;

            // High‑resolution output (300 DPI)
            converter.Resolution = new Resolution(300);

            // Prepare the conversion
            converter.DoConvert();

            // Save each selected page as a PNG image
            for (int pageNumber = converter.StartPage; pageNumber <= converter.EndPage; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"Page_{pageNumber}.png");
                // GetNextImage infers the format from the file extension
                converter.GetNextImage(outputPath);
                Console.WriteLine($"Saved page {pageNumber} as PNG → {outputPath}");
            }
        }

        Console.WriteLine("Conversion completed.");
    }
}
