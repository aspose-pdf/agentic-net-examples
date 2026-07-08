using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string dataDir = @"YOUR_DATA_DIRECTORY";
        const string pdfFile = @"YOUR_PDF_FILE";

        string pdfPath = Path.Combine(dataDir, pdfFile);
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Define the resolution (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Create a PNG device with the specified resolution
            PngDevice pngDevice = new PngDevice(resolution)
            {
                // Enable transparent background for the PNG images
                TransparentBackground = true
            };

            // Convert each page to a PNG file
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outPath = Path.Combine(dataDir, $"image{pageNumber}_out.png");
                using (FileStream pngStream = new FileStream(outPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                }
            }
        }

        Console.WriteLine("PDF successfully converted to PNG images with transparent background.");
    }
}