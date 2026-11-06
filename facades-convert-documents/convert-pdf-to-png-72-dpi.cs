using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "PreviewImages";
        const int dpi = 72; // quick preview resolution

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (wrap in using for deterministic disposal)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            int pageCount = pdfDocument.Pages.Count;
            Resolution resolution = new Resolution(dpi);

            // Iterate through each page and save as PNG using PngDevice
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"Page_{pageNumber}.png");

                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
                {
                    // PngDevice handles PNG conversion; it respects the supplied resolution (DPI)
                    PngDevice pngDevice = new PngDevice(resolution);
                    pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as PNG → {outputPath}");
            }
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}
