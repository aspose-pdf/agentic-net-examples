using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ConvertedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document.
        Document pdfDocument = new Document(inputPdf);

        // Define the page range (2‑6) – ensure we do not exceed the actual page count.
        int startPage = 2;
        int endPage   = Math.Min(6, pdfDocument.Pages.Count);

        // Set the desired resolution for the JPEG output.
        Resolution resolution = new Resolution(150);

        // JpegDevice renders PDF pages to JPEG images.
        // The constructor overload allows setting JPEG quality directly (no ImageQuality property).
        // CoordinateType is not required – the device uses CropBox by default.
        JpegDevice jpegDevice = new JpegDevice(resolution, 90); // 90 = JPEG quality (0‑100)

        // Convert each page in the selected range to a JPEG file.
        for (int pageNumber = startPage; pageNumber <= endPage; pageNumber++)
        {
            string outputPath = Path.Combine(outputDir, $"Page_{pageNumber}.jpg");
            using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
            {
                jpegDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
            }
            Console.WriteLine($"Saved page {pageNumber} as JPEG to '{outputPath}'.");
        }
    }
}
