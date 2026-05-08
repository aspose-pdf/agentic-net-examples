using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // for JpegDevice and Resolution

class PdfToJpegConverter
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";   // source PDF
        const string outputFolder = "Images";      // folder for JPEGs

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdfPath);

        // Set resolution to 72 DPI – 1 point = 1 pixel, preserving original page size.
        // The Resolution class lives in Aspose.Pdf.Devices namespace.
        Resolution resolution = new Resolution(72);

        // JpegDevice does NOT implement IDisposable, so do NOT wrap it in a using block.
        JpegDevice jpegDevice = new JpegDevice(resolution);

        int pageIndex = 1;
        foreach (Page page in pdfDocument.Pages)
        {
            string outputFile = Path.Combine(outputFolder, $"page_{pageIndex}.jpg");

            // Process the page directly to a file path.
            jpegDevice.Process(page, outputFile);

            pageIndex++;
        }

        Console.WriteLine($"Conversion completed. JPEG images saved to '{outputFolder}'.");
    }
}
