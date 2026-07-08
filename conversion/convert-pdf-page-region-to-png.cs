using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";   // source PDF
        const string pngPath   = "region.png";  // output PNG
        const int    pageIndex = 1;             // 1‑based page number

        // Define the region to extract (left, bottom, right, top) in points.
        // Adjust these values to the desired rectangle.
        const double left   = 100;
        const double bottom = 200;
        const double right  = 300;
        const double top    = 400;

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document.
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Ensure the requested page exists.
            if (pageIndex < 1 || pageIndex > pdfDocument.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            Page page = pdfDocument.Pages[pageIndex];

            // Preserve the original CropBox so we can restore it later.
            Aspose.Pdf.Rectangle originalCropBox = page.CropBox;

            // Set the CropBox to the desired region.
            // CropBox expects a rectangle defined by lower‑left (llx,lly) and upper‑right (urx,ury) coordinates.
            page.CropBox = new Aspose.Pdf.Rectangle(left, bottom, right, top);

            // Create a PNG device with desired resolution (e.g., 300 DPI).
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution);

            // Convert the cropped page to a PNG image stored in a memory stream.
            using (MemoryStream pngStream = new MemoryStream())
            {
                pngDevice.Process(page, pngStream);
                // Write the PNG bytes to the output file.
                File.WriteAllBytes(pngPath, pngStream.ToArray());
            }

            // Restore the original CropBox (optional, in case the document is used later).
            page.CropBox = originalCropBox;
        }

        Console.WriteLine($"Region saved as PNG: {pngPath}");
    }
}