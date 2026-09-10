using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // for Resolution and PngDevice

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF file
        const string outputDir = "output_images";    // folder for PNG files

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Set the desired resolution (300 DPI)
        Resolution resolution = new Resolution(300);
        // Create a PNG device with the specified resolution
        PngDevice pngDevice = new PngDevice(resolution);

        // Iterate through each page and save it as a PNG image
        for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
            using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
            {
                pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
            }
        }

        Console.WriteLine("PDF conversion to PNG completed.");
    }
}
