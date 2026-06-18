using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF and output PNG paths
        const string pdfPath = "input.pdf";
        const string pngPath = "region_output.png";

        // Define the rectangle region (coordinates in points, 1 point = 1/72 inch)
        // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
        Aspose.Pdf.Rectangle region = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Ensure the document has at least one page
            if (pdfDocument.Pages.Count < 1)
            {
                Console.Error.WriteLine("The PDF does not contain any pages.");
                return;
            }

            // Get the first page (or any page you need; pages are 1‑based)
            Page page = pdfDocument.Pages[1];

            // Preserve the original CropBox so we can restore it later
            Aspose.Pdf.Rectangle originalCropBox = page.CropBox;

            // Set the CropBox to the desired region – this defines the visible area
            page.CropBox = region;

            // Create a PNG device (default resolution 150 DPI; you can customize if needed)
            PngDevice pngDevice = new PngDevice();

            // Convert the cropped page to a PNG image in a memory stream
            using (MemoryStream pngStream = new MemoryStream())
            {
                pngDevice.Process(page, pngStream);
                // Write the PNG bytes to the output file
                File.WriteAllBytes(pngPath, pngStream.ToArray());
            }

            // Restore the original CropBox (optional, in case the document is used later)
            page.CropBox = originalCropBox;
        }

        Console.WriteLine($"Region PNG saved to '{pngPath}'.");
    }
}