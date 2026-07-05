using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // Added for ImageFormat

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

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfConverter is a Facade that implements IDisposable
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdf);

            // Convert only the first 10 pages (StartPage defaults to 1)
            converter.StartPage = 1;
            converter.EndPage   = 10;

            // Optional: set resolution if higher quality is needed
            // converter.Resolution = new Resolution(150);

            // Perform initial conversion setup
            converter.DoConvert();

            int pageNumber = 1;
            // Iterate through generated images
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");
                // Save the current page as BMP
                converter.GetNextImage(outputPath, ImageFormat.Bmp);
                pageNumber++;
            }
        }

        Console.WriteLine("PDF to BMP conversion completed.");
    }
}
