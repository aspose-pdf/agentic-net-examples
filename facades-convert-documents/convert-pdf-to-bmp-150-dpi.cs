using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "output";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(inputPath))
        {
            // Set resolution to 150 DPI
            Aspose.Pdf.Devices.Resolution resolution = new Aspose.Pdf.Devices.Resolution(150);
            // Initialize BMP device with the specified resolution
            Aspose.Pdf.Devices.BmpDevice bmpDevice = new Aspose.Pdf.Devices.BmpDevice(resolution);

            // Convert pages 1‑20 (or fewer if the document has less pages)
            int maxPage = Math.Min(20, pdfDocument.Pages.Count);
            for (int pageNumber = 1; pageNumber <= maxPage; pageNumber++)
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");
                using (FileStream bmpStream = new FileStream(outputPath, FileMode.Create))
                {
                    // Render the page to BMP and write to the stream
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }
                Console.WriteLine($"Saved page {pageNumber} as BMP to {outputPath}");
            }
        }
    }
}