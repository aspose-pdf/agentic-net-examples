using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class PdfToPngConverter
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputFolder = "PagesAsPng";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        Document pdfDocument = new Document(pdfPath);

        // Define the resolution for the PNG images (e.g., 150 DPI)
        var resolution = new Resolution(150);
        // Image devices (PngDevice, GifDevice, etc.) are not IDisposable – instantiate directly.
        var pngDevice = new PngDevice(resolution);

        // Iterate through each page and render it as PNG using the CropBox area
        for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            // Only the stream needs disposing.
            using (var imageStream = new MemoryStream())
            {
                // Render the current page into the memory stream
                pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);

                // Write the PNG file to disk
                string outPath = Path.Combine(outputFolder, $"Page_{pageNumber}.png");
                File.WriteAllBytes(outPath, imageStream.ToArray());
                Console.WriteLine($"Saved page {pageNumber} as PNG: {outPath}");
            }
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}