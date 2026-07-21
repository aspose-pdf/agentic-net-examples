using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output raster image file path (PNG format)
        const string outputImagePath = "page1.png";
        // Desired resolution in DPI (dots per inch)
        const int resolutionDpi = 300;

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Ensure the document contains at least one page (Aspose.Pdf uses 1‑based indexing)
            if (pdfDocument.Pages.Count < 1)
            {
                Console.Error.WriteLine("The PDF does not contain any pages.");
                return;
            }

            // Retrieve the first page (or change the index to target another page)
            Page page = pdfDocument.Pages[1];

            // Create a Resolution object with the required DPI
            Resolution resolution = new Resolution(resolutionDpi);

            // Initialize a PNG device with the specified resolution.
            // The device will rasterize the page (including all vector graphics) to PNG.
            PngDevice pngDevice = new PngDevice(resolution);

            // Rasterize the page and write the PNG data to the output file
            using (FileStream outputStream = new FileStream(outputImagePath, FileMode.Create))
            {
                pngDevice.Process(page, outputStream);
            }

            Console.WriteLine($"Page rasterized successfully to '{outputImagePath}' at {resolutionDpi} DPI.");
        }
    }
}