using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // for Resolution and JpegDevice

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output directory for JPEG images
        const string outputDir = "JpegImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Set the desired resolution (96 DPI) for web‑friendly images
        Resolution resolution = new Resolution(96);

        // Iterate through each page and convert it to a JPEG image
        for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            string outputPath = Path.Combine(outputDir, $"image{pageNumber}.jpg");

            using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
            {
                // JpegDevice renders the page to a JPEG image using the specified resolution
                JpegDevice jpegDevice = new JpegDevice(resolution);
                jpegDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
            }
        }

        Console.WriteLine($"PDF pages have been converted to JPEG images at '{outputDir}'.");
    }
}
