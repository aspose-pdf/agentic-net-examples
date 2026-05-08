using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "output_images";

        // Verify that the source PDF exists before proceeding
        if (!File.Exists(inputPdf))
        {
            Console.WriteLine($"Input file '{inputPdf}' not found.");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (Document implements IDisposable)
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Desired resolution – 72 DPI
            Resolution resolution = new Resolution(72);

            // Iterate through each page and render it as a PNG image.
            // PngDevice uses the CropBox by default, preserving the visible content.
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
                {
                    PngDevice pngDevice = new PngDevice(resolution);
                    pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                }
            }
        }

        Console.WriteLine("PDF conversion to PNG completed.");
    }
}