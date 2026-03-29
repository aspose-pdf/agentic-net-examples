using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load PDF document
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Create resolution of 150 DPI
            Resolution resolution = new Resolution(150);
            // Initialize PNG device with the resolution
            PngDevice pngDevice = new PngDevice(resolution);
            // Enable transparent background for the output images
            pngDevice.TransparentBackground = true;

            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputFile = $"page{pageNumber}.png";
                using (FileStream pngStream = new FileStream(outputFile, FileMode.Create))
                {
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                }
            }
        }

        Console.WriteLine("Conversion completed.");
    }
}