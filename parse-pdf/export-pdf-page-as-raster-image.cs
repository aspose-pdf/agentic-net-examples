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
        const int resolutionDpi = 300; // Desired resolution in DPI

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Ensure the document has at least one page (pages are 1‑based)
            if (pdfDocument.Pages.Count < 1)
            {
                Console.Error.WriteLine("The PDF does not contain any pages.");
                return;
            }

            // Optional: flatten transparency so that transparent content is rasterized correctly
            pdfDocument.FlattenTransparency();

            // Get the first page (or any page you need)
            Page page = pdfDocument.Pages[1];

            // Create a Resolution object with the desired DPI
            Resolution resolution = new Resolution(resolutionDpi);

            // Convert the page (including all vector graphics) to a raster image byte array
            byte[] rasterBytes = page.AsByteArray(resolution);

            // Save the raster image to a file (PNG format by default)
            File.WriteAllBytes(outputImagePath, rasterBytes);
        }

        Console.WriteLine($"Raster image exported successfully to '{outputImagePath}'.");
    }
}