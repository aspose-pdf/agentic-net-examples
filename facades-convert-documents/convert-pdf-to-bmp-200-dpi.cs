using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "output_images";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load PDF document within a using block (lifecycle rule)
        using (Document pdfDocument = new Document(inputPath))
        {
            // Create a resolution of 200 DPI
            Resolution resolution = new Resolution(200);
            // Initialize BmpDevice with the specified resolution
            BmpDevice bmpDevice = new BmpDevice(resolution);
            // Default CoordinateType uses CropBox, no need to change

            // Convert each page to a BMP file
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");
                using (FileStream bmpStream = new FileStream(outPath, FileMode.Create))
                {
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }
            }
        }

        Console.WriteLine("PDF to BMP conversion completed.");
    }
}