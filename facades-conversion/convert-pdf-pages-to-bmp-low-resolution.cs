using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "BmpPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // PdfConverter implements IDisposable – wrap in using for deterministic cleanup
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document to the converter
            converter.BindPdf(inputPdfPath);

            // Convert all pages
            converter.StartPage = 1;
            converter.EndPage   = converter.PageCount; // total pages after binding

            // Set low resolution (72 DPI) for image conversion
            converter.Resolution = new Resolution(72);

            // Prepare the conversion
            converter.DoConvert();

            // Export each page as a BMP image. GetNextImage infers the format from the file extension.
            for (int pageNumber = converter.StartPage; pageNumber <= converter.EndPage; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.bmp");
                converter.GetNextImage(outputPath);
                Console.WriteLine($"Page {pageNumber} saved as BMP: {outputPath}");
            }
        }

        Console.WriteLine("All pages have been converted to BMP images.");
    }
}
