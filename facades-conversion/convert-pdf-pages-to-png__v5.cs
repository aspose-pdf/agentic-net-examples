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
        const string outputFolder = "OutputImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // PdfConverter implements IDisposable, so wrap it in a using block
        using (PdfConverter converter = new PdfConverter())
        {
            // Load the PDF document
            converter.BindPdf(inputPdf);

            // Define the page range to convert (pages 2‑4)
            converter.StartPage = 2;
            converter.EndPage   = 4;

            // Optional: set resolution for the rasterized images
            converter.Resolution = new Resolution(150);

            // Prepare the conversion – this must be called before retrieving images
            converter.DoConvert();

            // Retrieve each image in the selected range as PNG
            int pageNumber = converter.StartPage;
            while (converter.HasNextImage())
            {
                string outPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");
                converter.GetNextImage(outPath, ImageFormat.Png);
                Console.WriteLine($"Saved page {pageNumber} as PNG to {outPath}");
                pageNumber++;
            }
        }
    }
}
