using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "JpegPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (Document implements IDisposable, so a using block is fine)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Define the resolution (96 DPI) and JPEG quality (0‑100)
            var resolution = new Resolution(96);
            const int jpegQuality = 90;

            // JpegDevice does NOT implement IDisposable – instantiate it directly
            var jpegDevice = new JpegDevice(resolution, jpegQuality);

            // Convert each page to a separate JPEG file
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.jpg");
                // Dispose only the stream; JpegDevice is reused for all pages
                using (var imageStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                }
                Console.WriteLine($"Page {pageNumber} saved as {outputPath}");
            }
        }

        Console.WriteLine("PDF to JPEG conversion completed.");
    }
}
