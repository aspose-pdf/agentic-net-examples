using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // Resolution, BmpDevice

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "BmpPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create a BmpDevice with the desired resolution.
            // Recent Aspose.Pdf versions use CropBox as the default coordinate type,
            // so we do not set CoordinateType explicitly (the property no longer exists).
            BmpDevice bmpDevice = new BmpDevice(new Resolution(300));

            // Convert each page to a BMP image.
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.bmp");
                // Process writes the bitmap directly to the file.
                bmpDevice.Process(pdfDoc.Pages[pageNumber], outputPath);
            }
        }

        Console.WriteLine("PDF pages have been successfully converted to BMP images.");
    }
}
