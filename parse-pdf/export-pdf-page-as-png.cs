using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // Image devices for rasterization

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";   // source PDF
        const string outputImagePath = "page1.png"; // raster image output
        const int dpi = 300; // desired resolution in dots per inch

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Verify that the requested page exists (pages are 1‑based)
            if (pdfDoc.Pages.Count < 1)
            {
                Console.Error.WriteLine("The PDF contains no pages.");
                return;
            }

            // Create a Resolution object with the specified DPI
            Resolution resolution = new Resolution(dpi);

            // Initialize a PNG device with the desired resolution
            PngDevice pngDevice = new PngDevice(resolution);

            // Rasterize the first page (or any page index) to a PNG file
            using (FileStream outStream = new FileStream(outputImagePath, FileMode.Create))
            {
                pngDevice.Process(pdfDoc.Pages[1], outStream);
            }

            Console.WriteLine($"Page 1 rasterized to '{outputImagePath}' at {dpi} DPI.");
        }
    }
}