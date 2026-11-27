using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Resolution struct
using System.Drawing.Imaging; // ImageFormat enum

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "GifPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfConverter implements IDisposable – wrap it in a using block
        using (PdfConverter converter = new PdfConverter())
        {
            // Load the PDF file
            converter.BindPdf(inputPdf);

            // Convert pages 2 through 4 (inclusive)
            converter.StartPage = 2;
            converter.EndPage   = 4;

            // Set the conversion resolution to 150 DPI (moderate resolution)
            converter.Resolution = new Resolution(150);

            // Perform the conversion – this prepares the internal image stream
            converter.DoConvert();

            int pageNumber = converter.StartPage;
            while (converter.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.gif");
                // Save the next page image as GIF
                converter.GetNextImage(outPath, ImageFormat.Gif);
                Console.WriteLine($"Saved page {pageNumber} as GIF to {outPath}");
                pageNumber++;
            }

            // Release any internal resources held by the converter
            converter.Close();
        }
    }
}
