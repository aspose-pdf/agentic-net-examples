using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "PngPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);
        int pageCount = pdfDocument.Pages.Count;

        // Configure the PNG device (resolution 72 DPI)
        // Note: The UseCropBox property is not available in recent Aspose.Pdf versions.
        // The device renders using the page's CropBox by default, so we omit the property.
        var pngDevice = new PngDevice(new Resolution(72));

        // Convert each page to a PNG image
        for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
        {
            string outPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
            using (FileStream imageStream = new FileStream(outPath, FileMode.Create))
            {
                pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
            }
            Console.WriteLine($"Saved page {pageNumber} as PNG to '{outPath}'.");
        }
    }
}
