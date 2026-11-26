using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "BmpPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // BmpDevice with default resolution (96 DPI)
            var bmpDevice = new BmpDevice(new Resolution(96));

            // Iterate using 1‑based page indexing (Aspose.Pdf uses 1‑based indexes)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                Page page = pdfDoc.Pages[pageNumber];

                // Build the output BMP file name for the current page
                string bmpPath = Path.Combine(outputFolder, $"page_{pageNumber}.bmp");

                // Render the page to the BMP file (devices are not IDisposable, so no using)
                bmpDevice.Process(page, bmpPath);

                Console.WriteLine($"Page {pageNumber} saved as BMP: {bmpPath}");
            }
        }

        Console.WriteLine("PDF to BMP conversion completed.");
    }
}
