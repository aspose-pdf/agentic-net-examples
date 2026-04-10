using System;
using System.IO;
using System.Drawing.Imaging; // Added for ImageFormat
using Aspose.Pdf;
using Aspose.Pdf.Facades; // PdfConverter resides here

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        string dataDir   = @"C:\PdfData";          // folder containing the source PDF
        string pdfFile   = "sample.pdf";           // source PDF file name
        string outputDir = @"C:\PdfData\BmpPages"; // folder to store BMP images

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Full path to the source PDF
        string pdfPath = Path.Combine(dataDir, pdfFile);
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        // Convert pages 3 to 8 of the PDF to BMP images using PdfConverter (Facade API)
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF file to the converter
            converter.BindPdf(pdfPath);

            // Define the page range (1‑based indexing)
            converter.StartPage = 3;
            converter.EndPage   = 8;

            // Prepare the converter for image extraction
            converter.DoConvert();

            // Iterate through the selected pages and save each as a BMP file
            int pageNumber = converter.StartPage;
            while (converter.HasNextImage())
            {
                string bmpPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");
                // Save the current page image as BMP using System.Drawing.Imaging.ImageFormat
                converter.GetNextImage(bmpPath, ImageFormat.Bmp);
                pageNumber++;
            }
        }

        Console.WriteLine("BMP conversion completed.");
    }
}
