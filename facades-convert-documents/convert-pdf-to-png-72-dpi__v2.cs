using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class PdfToPngConverter
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "output_images";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdfPath);

        // Desired resolution – 72 DPI for both axes
        var resolution = new Resolution(72);

        // PngDevice respects the CropBox of each page automatically
        var pngDevice = new PngDevice(resolution);

        for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            using (var ms = new MemoryStream())
            {
                // Convert the current page to PNG and write to the memory stream
                pngDevice.Process(pdfDocument.Pages[pageNumber], ms);

                // Build the output file name (e.g., Page_1.png)
                string outputPath = Path.Combine(outputFolder, $"Page_{pageNumber}.png");

                // Save the PNG bytes to disk
                File.WriteAllBytes(outputPath, ms.ToArray());
                Console.WriteLine($"Saved page {pageNumber} to '{outputPath}'.");
            }
        }
    }
}
