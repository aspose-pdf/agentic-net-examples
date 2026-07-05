using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // BmpDevice and Resolution classes reside here

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "BmpPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdfPath);

        // Determine the range of pages to convert (1‑20 or up to the last page)
        int startPage = 1;
        int endPage   = Math.Min(20, pdfDocument.Pages.Count);

        // Set the desired resolution (150 DPI)
        Resolution resolution = new Resolution(150);

        // BmpDevice does NOT implement IDisposable, so instantiate it without a using block
        BmpDevice bmpDevice = new BmpDevice(resolution);

        for (int pageNumber = startPage; pageNumber <= endPage; pageNumber++)
        {
            string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.bmp");

            // Dispose only the FileStream; the device is reused for all pages
            using (FileStream imageStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // Render the current page to the BMP image stream
                bmpDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
            }
        }

        Console.WriteLine("BMP conversion completed.");
    }
}
