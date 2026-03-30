using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPrefix = "image";
        const string outputSuffix = ".jpg";
        const int jpegQuality = 85;               // Desired JPEG quality (0‑100)
        const int resolutionDpi = 150;            // Reasonable DPI for good clarity

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: File '{inputPdf}' not found.");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdf))
        {
            // JpegDevice does NOT implement IDisposable, so do NOT wrap it in a using block
            JpegDevice jpegDevice = new JpegDevice(new Resolution(resolutionDpi), jpegQuality);

            int imageCount = 1;
            // Iterate through each page and render it as a JPEG image
            foreach (Page page in pdfDocument.Pages)
            {
                string outputFile = $"{outputPrefix}{imageCount}{outputSuffix}";

                // Process the page directly to a file path (or you could use a FileStream if preferred)
                jpegDevice.Process(page, outputFile);

                imageCount++;
            }
        }
    }
}
