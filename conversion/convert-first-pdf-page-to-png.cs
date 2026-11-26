using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPng = "page1.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Verify that the document has at least one page (pages are 1‑based)
                if (pdfDoc.Pages.Count < 1)
                {
                    Console.Error.WriteLine("The PDF contains no pages.");
                    return;
                }

                // PngDevice renders PDF pages to PNG images.
                // Devices are concrete but do NOT implement IDisposable, so do NOT wrap them in a using block.
                // Resolution can be set via the constructor; TransparentBackground is optional.
                var pngDevice = new PngDevice(new Resolution(300))
                {
                    TransparentBackground = true
                };

                // Process the first page and save it as a PNG file
                pngDevice.Process(pdfDoc.Pages[1], outputPng);
            }

            Console.WriteLine($"First page saved as PNG: {outputPng}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
