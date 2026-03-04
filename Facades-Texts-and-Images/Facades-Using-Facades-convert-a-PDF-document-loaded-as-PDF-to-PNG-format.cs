using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Create and configure the PdfConverter facade
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdf);

            // Initialize conversion process
            converter.DoConvert();

            int pageNumber = 1;
            // Loop through each page and save as PNG
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");
                converter.GetNextImage(outputPath, ImageFormat.Png);
                pageNumber++;
            }
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}