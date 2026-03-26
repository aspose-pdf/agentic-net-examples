using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPattern = "page_{0}.jpeg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // JpegDevice with output JPEG quality set to 80
        JpegDevice jpegDevice = new JpegDevice(80);

        using (Document pdfDocument = new Document(inputPdf))
        {
            // Optimize resources with default (high) compression. The API no longer accepts
            // OptimizationOptions, and ImageCompressionOptions is read‑only.
            pdfDocument.OptimizeResources();

            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = string.Format(outputPattern, pageNumber);
                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
                {
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                }
                Console.WriteLine($"Saved page {pageNumber} as {outputPath}");
            }
        }
    }
}
