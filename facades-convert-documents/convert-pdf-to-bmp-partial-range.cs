using System;
using System.IO;
using Aspose.Pdf.Facades;
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

        // Use PdfConverter facade to convert pages to BMP images
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdf);

            // Specify the page range (2 to 6, inclusive)
            converter.StartPage = 2;
            converter.EndPage   = 6;

            // Prepare the conversion process
            converter.DoConvert();

            int pageNumber = converter.StartPage;
            while (converter.HasNextImage())
            {
                // Build the output file name for each page
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");

                // Save the current page as a BMP image
                converter.GetNextImage(outputPath, ImageFormat.Bmp);

                pageNumber++;
            }
        }

        Console.WriteLine("PDF to BMP conversion completed.");
    }
}