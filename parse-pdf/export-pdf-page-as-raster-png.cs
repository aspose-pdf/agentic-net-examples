using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class ExportVectorPageAsRaster
{
    /// <summary>
    /// Exports the specified page of a PDF as a raster image (PNG) with the given resolution (DPI).
    /// All vector graphics on the page are rasterized into the resulting image.
    /// </summary>
    /// <param name="pdfPath">Path to the source PDF file.</param>
    /// <param name="outputImagePath">Path where the raster image will be saved.</param>
    /// <param name="pageNumber">1‑based page index to export.</param>
    /// <param name="dpi">Resolution in dots per inch.</param>
    public static void ExportPage(string pdfPath, string outputImagePath, int pageNumber, int dpi)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Validate page number (Aspose.Pdf uses 1‑based indexing).
            if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page number {pageNumber}. Document has {pdfDoc.Pages.Count} pages.");
                return;
            }

            // Create a Resolution object with the desired DPI.
            Resolution resolution = new Resolution(dpi);

            // Initialize a PNG device with the specified resolution.
            // The device will render the page to a PNG image.
            PngDevice pngDevice = new PngDevice(resolution);

            // Process the selected page and write the image to the output file.
            // The Process method handles all content, including vector graphics,
            // converting them into raster pixels.
            pngDevice.Process(pdfDoc.Pages[pageNumber], outputImagePath);
        }

        Console.WriteLine($"Page {pageNumber} exported to raster image: {outputImagePath}");
    }

    // Example usage
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string outputPng = "page1.png";
        const int pageToExport = 1;   // first page
        const int desiredDpi = 300;   // high‑resolution rasterization

        ExportPage(inputPdf, outputPng, pageToExport, desiredDpi);
    }
}