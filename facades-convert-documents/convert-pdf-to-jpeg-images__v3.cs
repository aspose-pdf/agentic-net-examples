using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "OutputImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);
        int pageCount = pdfDocument.Pages.Count;

        // Process each page sequentially and save as JPEG
        for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
        {
            string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpg");

            // Create a JPEG device with the desired resolution (150 DPI)
            var jpegDevice = new JpegDevice(new Resolution(150));

            // Convert the current page to JPEG and write directly to a file stream
            using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
            {
                jpegDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
            }
        }

        Console.WriteLine("PDF pages have been converted to JPEG images.");
    }
}
