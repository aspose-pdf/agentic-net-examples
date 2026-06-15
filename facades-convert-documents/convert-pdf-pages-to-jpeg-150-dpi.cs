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
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Determine how many pages we actually have (max 10)
            int totalPages = Math.Min(10, pdfDocument.Pages.Count);

            // JpegDevice expects a Resolution object for DPI.
            // The second argument is the image quality (0‑100). 100 = best quality.
            Resolution resolution = new Resolution(150); // 150 DPI for both X and Y
            var jpegDevice = new JpegDevice(resolution, 100);

            for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpg");

                // The JpegDevice respects the page's CropBox when rendering.
                // If you need to enforce it explicitly you can set:
                // pdfDocument.Pages[pageNumber].CropBox = pdfDocument.Pages[pageNumber].CropBox;
                // but the default behaviour already uses the CropBox.

                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
                {
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                }
            }
        }

        Console.WriteLine("PDF pages 1‑10 have been converted to JPEG images.");
    }
}
