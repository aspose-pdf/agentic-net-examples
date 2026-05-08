using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Use a using declaration for automatic disposal of PdfConverter
        using (PdfConverter converter = new PdfConverter())
        {
            converter.BindPdf(inputPdf);
            // Set DPI using Aspose.Pdf.Devices.Resolution (72 DPI for quick preview)
            converter.Resolution = new Resolution(72);
            converter.DoConvert();

            int pageNumber = 1;
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
                // GetNextImage writes the image directly to the specified file path
                converter.GetNextImage(outputPath);
                pageNumber++;
            }
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}
