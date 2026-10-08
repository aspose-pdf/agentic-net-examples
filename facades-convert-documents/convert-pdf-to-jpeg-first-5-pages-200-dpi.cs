using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "Images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Determine how many pages to process (max 5)
        int totalPages = pdfDocument.Pages.Count;
        int endPage = Math.Min(5, totalPages);

        // Set resolution to 200 DPI for JPEG conversion
        Resolution resolution = new Resolution(200);

        // Convert each selected page to a JPEG image
        for (int pageNumber = 1; pageNumber <= endPage; pageNumber++)
        {
            string outputPath = Path.Combine(outputFolder, $"Page_{pageNumber}.jpg");
            using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
            {
                // JpegDevice handles per‑page conversion
                JpegDevice jpegDevice = new JpegDevice(resolution);
                jpegDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
            }
            Console.WriteLine($"Saved page {pageNumber} as JPEG to '{outputPath}'.");
        }

        Console.WriteLine("Conversion completed.");
    }
}
