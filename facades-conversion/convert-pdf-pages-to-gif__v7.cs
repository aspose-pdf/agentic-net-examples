using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "GifPages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfConverter to convert each page to a GIF image
        using (var converter = new PdfConverter())
        {
            // Bind the PDF document
            converter.BindPdf(inputPdfPath);

            // Configure conversion range and resolution
            converter.StartPage = 1;                                 // 1‑based indexing
            converter.EndPage   = converter.PageCount;               // all pages
            converter.Resolution = new Resolution(150);              // DPI (adjust as needed)

            // Prepare the conversion
            converter.DoConvert();

            // Iterate through the generated images
            int pageNumber = converter.StartPage;
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"Page_{pageNumber}.gif");
                // Save the current page as GIF
                converter.GetNextImage(outputPath, ImageFormat.Gif);
                Console.WriteLine($"Saved page {pageNumber} as {outputPath}");
                pageNumber++;
            }

            // Release resources
            converter.Close();
        }
    }
}
