using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPng = "page7.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Verify that page 7 exists
        const int pageNumber = 7;
        if (pdfDocument.Pages.Count < pageNumber)
        {
            Console.Error.WriteLine($"PDF does not contain page {pageNumber}.");
            return;
        }

        // Create a PNG device with a resolution of 300 DPI.
        // Image devices (PngDevice, JpegDevice, etc.) do NOT implement IDisposable,
        // so they must NOT be used inside a using statement.
        var pngDevice = new PngDevice(new Resolution(300))
        {
            TransparentBackground = true // optional: keep background transparent
        };

        // Process the specified page and write the PNG to a file stream.
        using (FileStream fs = new FileStream(outputPng, FileMode.Create, FileAccess.Write))
        {
            pngDevice.Process(pdfDocument.Pages[pageNumber], fs);
        }

        Console.WriteLine($"Page {pageNumber} has been saved as PNG to '{outputPng}'.");
    }
}
