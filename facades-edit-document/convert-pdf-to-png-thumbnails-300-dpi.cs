using System;
using System.IO;
using Aspose.Pdf;                     // Document
using Aspose.Pdf.Devices;             // Resolution, PngDevice

class PdfToPngThumbnail
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Directory where PNG thumbnails will be saved
        const string outputDir = "Thumbnails";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Verify the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdf))
        {
            int pageCount = pdfDocument.Pages.Count;
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");

                // Create a PNG device with 300 DPI and transparent background
                PngDevice pngDevice = new PngDevice(new Resolution(300))
                {
                    TransparentBackground = true
                };

                // Convert the specific page to PNG
                pngDevice.Process(pdfDocument.Pages[pageNumber], outputPath);
            }
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}
