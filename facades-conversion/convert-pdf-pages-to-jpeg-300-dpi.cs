using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "JpegPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Configure the desired resolution (300 DPI) and quality (0‑100)
        Resolution resolution = new Resolution(300);
        // JpegDevice constructor accepts both resolution and quality; there is no "Quality" property.
        JpegDevice jpegDevice = new JpegDevice(resolution, 90);

        // Convert each page to a JPEG image
        for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            string outputPath = Path.Combine(outputDir, $"Page_{pageNumber}.jpg");
            using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
            {
                jpegDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
            }
            Console.WriteLine($"Page {pageNumber} saved as JPEG: {outputPath}");
        }

        Console.WriteLine("All pages have been converted to JPEG images.");
    }
}
