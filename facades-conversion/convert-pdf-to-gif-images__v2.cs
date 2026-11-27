using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;
using System.Drawing.Imaging;

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

        Directory.CreateDirectory(outputDir);

        try
        {
            // Initialize the PdfConverter facade and bind the PDF document
            using (PdfConverter converter = new PdfConverter())
            {
                converter.BindPdf(inputPdf);

                // Configure conversion parameters
                converter.StartPage = 1;
                converter.EndPage   = converter.PageCount; // convert all pages
                converter.Resolution = new Resolution(150); // DPI – adjust as needed

                // Prepare the conversion
                converter.DoConvert();

                // Export each page as a GIF image
                int pageNumber = converter.StartPage;
                while (converter.HasNextImage())
                {
                    string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.gif");
                    converter.GetNextImage(outPath, ImageFormat.Gif);
                    Console.WriteLine($"Saved page {pageNumber} as GIF → {outPath}");
                    pageNumber++;
                }

                // Release all resources held by the converter
                converter.Close();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
