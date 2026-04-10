using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Convert each PDF page to a separate PNG using PdfConverter
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF file to the converter
            converter.BindPdf(inputPdf);
            // Initialize conversion process
            converter.DoConvert();

            int pageNumber = 1;
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
                // Save the current page as PNG
                converter.GetNextImage(outputPath, ImageFormat.Png);
                pageNumber++;
            }
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}