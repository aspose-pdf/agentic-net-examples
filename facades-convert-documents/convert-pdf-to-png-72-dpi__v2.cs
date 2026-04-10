using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class PdfToPngConverter
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        using (Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(inputPdf))
        {
            // Set resolution to 72 DPI
            Aspose.Pdf.Devices.Resolution resolution = new Aspose.Pdf.Devices.Resolution(72);

            // Create a PNG device; CropBox is used by default
            Aspose.Pdf.Devices.PngDevice pngDevice = new Aspose.Pdf.Devices.PngDevice(resolution);

            // Iterate through all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");

                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
                {
                    // Convert the current page to PNG and write to the file
                    pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                }
            }
        }

        Console.WriteLine("PDF successfully converted to PNG images.");
    }
}