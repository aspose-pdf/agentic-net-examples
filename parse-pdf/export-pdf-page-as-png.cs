using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputImagePath = "page1.png";
        const int dpi = 300; // Desired resolution in dots per inch

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Verify that the document contains at least one page
            if (pdfDoc.Pages.Count < 1)
            {
                Console.Error.WriteLine("The PDF does not contain any pages.");
                return;
            }

            // Create a Resolution object with the specified DPI
            Resolution resolution = new Resolution(dpi);

            // Initialize a PNG device with the desired resolution
            PngDevice pngDevice = new PngDevice(resolution);

            // Rasterize the first page and write the PNG bytes to a file
            using (FileStream outputStream = new FileStream(outputImagePath, FileMode.Create))
            {
                pngDevice.Process(pdfDoc.Pages[1], outputStream);
            }

            Console.WriteLine($"Page 1 rasterized to '{outputImagePath}' at {dpi} DPI.");
        }
    }
}