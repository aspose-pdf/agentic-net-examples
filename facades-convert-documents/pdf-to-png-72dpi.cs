using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Set resolution to 72 DPI
        Resolution resolution = new Resolution(72);
        // Initialize PNG device with the specified resolution
        PngDevice pngDevice = new PngDevice(resolution);
        // No need to set CoordinateType – CropBox is used by default in recent Aspose.Pdf versions

        using (Document pdfDocument = new Document(inputPath))
        {
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputFile = $"image{pageNumber}.png";
                using (FileStream pngStream = new FileStream(outputFile, FileMode.Create))
                {
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                }
            }
        }

        Console.WriteLine("PDF pages have been converted to PNG images.");
    }
}
