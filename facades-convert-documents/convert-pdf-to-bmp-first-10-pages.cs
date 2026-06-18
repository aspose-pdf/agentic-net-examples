using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // for BmpDevice and Resolution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "BmpImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Determine how many pages we will process (up to 10 or the total page count)
        int pagesToConvert = Math.Min(10, pdfDocument.Pages.Count);

        // Optional: set a higher resolution for better image quality
        Resolution resolution = new Resolution(150);
        // Create a BMP device with the desired resolution
        BmpDevice bmpDevice = new BmpDevice(resolution);

        for (int pageNumber = 1; pageNumber <= pagesToConvert; pageNumber++)
        {
            // Build the output BMP file name
            string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.bmp");

            // Convert the current page to a BMP image using the BmpDevice
            using (FileStream imageStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                bmpDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
            }
        }

        Console.WriteLine("PDF to BMP conversion completed.");
    }
}
