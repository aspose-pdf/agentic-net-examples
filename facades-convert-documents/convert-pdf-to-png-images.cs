using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Pages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdf);
            int pageCount = pdfDocument.Pages.Count;

            // Define the resolution for the PNG images (e.g., 150 DPI)
            var resolution = new Resolution(150);

            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
                using (FileStream imageStream = new FileStream(outPath, FileMode.Create))
                {
                    // Convert the specific page to PNG using PngDevice
                    var pngDevice = new PngDevice(resolution);
                    pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                }
                Console.WriteLine($"Saved page {pageNumber} → {outPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
