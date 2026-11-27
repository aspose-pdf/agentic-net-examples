using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // JpegDevice does NOT implement IDisposable, so instantiate it directly
        JpegDevice jpegDevice = new JpegDevice(); // default configuration (300 DPI)

        // Convert each page to a JPEG image
        for (int i = 1; i <= pdfDocument.Pages.Count; i++)
        {
            string outPath = Path.Combine(outputDir, $"Page_{i}.jpg");
            jpegDevice.Process(pdfDocument.Pages[i], outPath);
        }

        Console.WriteLine($"Conversion complete. JPEG images saved to '{outputDir}'.");
    }
}