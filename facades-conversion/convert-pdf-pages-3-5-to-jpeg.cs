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
        const string outputFolder = "Images";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // PdfConverter implements IDisposable – wrap it in a using block
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF document
            converter.BindPdf(inputPdfPath);

            // Configure the page range to convert (pages are 1‑based)
            converter.StartPage = 3;
            converter.EndPage   = 5;

            // Optional: set rasterization resolution
            converter.Resolution = new Resolution(150);

            // Prepare the conversion
            converter.DoConvert();

            // Loop through the selected pages and save each as a JPEG image
            int pageNumber = converter.StartPage;
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"Page_{pageNumber}.jpg");
                // Save the current page as JPEG
                converter.GetNextImage(outputPath, ImageFormat.Jpeg);
                Console.WriteLine($"Saved page {pageNumber} as JPEG: {outputPath}");
                pageNumber++;
            }

            converter.Close();
        }
    }
}
