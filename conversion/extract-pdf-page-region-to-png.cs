using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output PNG file path
        const string outputPng = "region.png";
        // Page number to extract (1‑based indexing)
        const int pageNumber = 1;

        // Ensure the input PDF exists – create a minimal placeholder if it does not.
        if (!File.Exists(inputPdf))
        {
            using (var placeholder = new Document())
            {
                placeholder.Pages.Add(); // add a blank page
                placeholder.Save(inputPdf);
            }
        }

        // Define the rectangle region (llx, lly, urx, ury) in points
        // Adjust these values to the desired area on the page
        Aspose.Pdf.Rectangle regionRect = new Aspose.Pdf.Rectangle(100, 200, 300, 400);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Get the target page
            Page page = pdfDoc.Pages[pageNumber];

            // Preserve the original CropBox so we can restore it later
            Aspose.Pdf.Rectangle originalCropBox = page.CropBox;

            // Set the CropBox to the region we want to render
            page.CropBox = regionRect;

            // Create a PNG device with desired resolution (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution);

            // Ensure the device uses the CropBox coordinates
            pngDevice.CoordinateType = PageCoordinateType.CropBox;

            // Render the page region to a PNG file
            using (FileStream outStream = new FileStream(outputPng, FileMode.Create))
            {
                pngDevice.Process(page, outStream);
            }

            // Restore the original CropBox (optional, in case the document is used later)
            page.CropBox = originalCropBox;
        }

        Console.WriteLine($"Region extracted to PNG: {outputPng}");
    }
}
