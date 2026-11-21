using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "PagesAsJpeg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (deterministic disposal via using)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Determine the page range (1‑10 or total page count)
            int startPage = 1;
            int endPage   = Math.Min(10, pdfDoc.Pages.Count);

            // Initialise the JPEG device with the required DPI.
            // The device uses the page's CropBox by default, so no PageSize property is needed.
            var jpegDevice = new JpegDevice(new Resolution(150));

            // Convert each page individually
            for (int pageNumber = startPage; pageNumber <= endPage; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.jpg");
                jpegDevice.Process(pdfDoc.Pages[pageNumber], outputPath);
                Console.WriteLine($"Page {pageNumber} saved as {outputPath}");
            }
        }

        Console.WriteLine("Conversion completed.");
    }
}