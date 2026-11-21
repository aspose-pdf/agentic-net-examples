using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "TiffPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document.
        Document doc = new Document(inputPdf);

        // Create a TiffDevice with the default resolution (96 DPI).
        // The constructor expects a Resolution, not a file path.
        TiffDevice tiffDevice = new TiffDevice(new Resolution(96));

        // Convert pages 3 through 8 (or up to the last page if the document is shorter).
        for (int page = 3; page <= 8 && page <= doc.Pages.Count; page++)
        {
            string outPath = Path.Combine(outputDir, $"Page_{page}.tiff");
            // Process a single page; the device uses the page's CropBox automatically.
            tiffDevice.Process(doc, page, page, outPath);
        }

        Console.WriteLine("Pages 3‑8 have been converted to TIFF images.");
    }
}
