using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF, page number and output PNG file
        const string inputPdfPath = "input.pdf";
        const string outputPngPath = "region.png";
        const int pageNumber = 1;               // 1‑based page index
        // Rectangle coordinates (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
        const double llx = 100;                 // left
        const double lly = 200;                 // bottom
        const double urx = 400;                 // right
        const double ury = 600;                 // top

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Ensure the requested page exists
            if (pageNumber < 1 || pageNumber > pdfDocument.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} is out of range. Document has {pdfDocument.Pages.Count} pages.");
                return;
            }

            // Define the resolution (DPI) for the raster image
            Resolution resolution = new Resolution(300);

            // Create a PNG device with the specified resolution
            PngDevice pngDevice = new PngDevice(resolution);

            // Get the target page
            Page page = pdfDocument.Pages[pageNumber];

            // Preserve the original CropBox so we can restore it after rendering
            var originalCropBox = page.CropBox;

            // Set the CropBox to the desired region (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            page.CropBox = new Rectangle(llx, lly, urx, ury);

            // Render the selected page region to a PNG file
            pngDevice.Process(page, outputPngPath);

            // Restore the original CropBox (optional, good practice if the document is used later)
            page.CropBox = originalCropBox;
        }

        Console.WriteLine($"Region of page {pageNumber} saved as PNG to '{outputPngPath}'.");
    }
}
