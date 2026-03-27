using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory containing the PDF file
        string dataDir = "Data";
        // PDF file name
        string pdfFile = "input.pdf";

        string pdfPath = Path.Combine(dataDir, pdfFile);
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Set desired resolution for the PNG images
            Resolution resolution = new Resolution(300);
            // Create a PNG device with the resolution
            PngDevice pngDevice = new PngDevice(resolution);
            // Use CropBox coordinates to capture only the visible page area
            pngDevice.CoordinateType = PageCoordinateType.CropBox;

            // Iterate through all pages and save each as a PNG file
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputFile = $"image{pageNumber}.png";
                using (FileStream pngStream = new FileStream(outputFile, FileMode.Create))
                {
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                }
            }
        }

        Console.WriteLine("PDF pages have been converted to PNG images using CropBox.");
    }
}