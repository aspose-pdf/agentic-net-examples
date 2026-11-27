using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "GifPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // PdfConverter implements IDisposable, wrap it in a using block
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document to the converter
            converter.BindPdf(inputPdf);

            // Configure conversion parameters (default resolution is 150 DPI)
            converter.StartPage = 1;
            converter.EndPage   = converter.PageCount; // convert all pages
            converter.Resolution = new Resolution(150); // default resolution

            // Prepare the conversion – no arguments are required
            converter.DoConvert();

            // Loop through the generated images and save each as a GIF
            int pageNumber = converter.StartPage;
            while (converter.HasNextImage())
            {
                string outPath = Path.Combine(outputFolder, $"page_{pageNumber}.gif");
                // GetNextImage writes the image to the specified path using the given format
                converter.GetNextImage(outPath, ImageFormat.Gif);
                Console.WriteLine($"Saved page {pageNumber} as GIF: {outPath}");
                pageNumber++;
            }
        }
    }
}
