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
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Initialize the PdfConverter facade
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the source PDF file
                converter.BindPdf(inputPdf);

                // Set the page range: start at page 3, end at page 8
                converter.StartPage = 3;
                converter.EndPage = 8;

                // Prepare the conversion process
                converter.DoConvert();

                int pageNumber = converter.StartPage;
                // Iterate through the selected pages and save each as BMP
                while (converter.HasNextImage())
                {
                    string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");
                    converter.GetNextImage(outputPath, ImageFormat.Bmp);
                    pageNumber++;
                }
            }

            Console.WriteLine("PDF to BMP conversion completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}