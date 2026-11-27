using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "CroppedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdfPath);

        // Define the resolution (DPI) for the JPEG images
        const int resolution = 150;
        const int jpegQuality = 90; // JPEG quality (0‑100)

        // Convert pages 2 through 5 (or up to the last page if the document is shorter)
        for (int pageNumber = 2; pageNumber <= 5 && pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            string outputImagePath = Path.Combine(outputFolder, $"page_{pageNumber}.jpg");

            // JpegDevice does NOT implement IDisposable – instantiate directly.
            // Use the constructor that accepts both resolution and quality.
            JpegDevice jpegDevice = new JpegDevice(resolution, jpegQuality);

            // Process the specific page; the device writes the JPEG file using the page's CropBox.
            jpegDevice.Process(pdfDocument.Pages[pageNumber], outputImagePath);

            Console.WriteLine($"Page {pageNumber} saved as JPEG: {outputImagePath}");
        }
    }
}