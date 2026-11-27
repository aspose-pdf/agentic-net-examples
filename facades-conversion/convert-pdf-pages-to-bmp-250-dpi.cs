using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "BmpPages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Create and configure the PdfConverter
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document
            converter.BindPdf(inputPath);

            // Set the page range and resolution
            converter.StartPage = 1;               // first page to convert
            converter.EndPage   = 8;               // last page to convert
            converter.Resolution = new Resolution(250); // 250 DPI

            // Prepare the conversion
            converter.DoConvert();

            // Convert each page to a BMP image
            for (int page = converter.StartPage; page <= converter.EndPage; page++)
            {
                string outPath = Path.Combine(outputDir, $"Page_{page}.bmp");
                // Save the current page as BMP; format is inferred from the file extension
                converter.GetNextImage(outPath);
            }

            // Release resources
            converter.Close();
        }
    }
}
