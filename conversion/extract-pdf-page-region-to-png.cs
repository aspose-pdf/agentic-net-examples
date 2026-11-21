using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string pngPath = "region.png";

        // Define the rectangle (in points, 1/72 inch) to extract
        double llx = 100; // lower‑left X
        double lly = 200; // lower‑left Y
        double urx = 400; // upper‑right X
        double ury = 600; // upper‑right Y

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document (Document must be disposed)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Pages are 1‑based
            Page page = pdfDoc.Pages[1];

            // Preserve original CropBox so we can restore it later
            var originalCropBox = page.CropBox;

            // Set the CropBox to the region we want to render
            page.CropBox = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Create a PNG device with the desired resolution (dpi)
            var pngDevice = new PngDevice(new Resolution(300));

            // Render the cropped region to a PNG file
            using (FileStream imageStream = new FileStream(pngPath, FileMode.Create))
            {
                pngDevice.Process(page, imageStream);
            }

            // Restore the original CropBox (good practice if the document is reused)
            page.CropBox = originalCropBox;
        }

        Console.WriteLine($"Region saved as PNG to '{pngPath}'.");
    }
}
