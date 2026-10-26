using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "TiffPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Set a default font fallback for missing characters using RenderingOptions
            RenderingOptions renderOptions = new RenderingOptions();
            renderOptions.DefaultFontName = "Arial"; // fallback font name

            // Iterate through all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                // Configure TIFF settings with LZW compression
                TiffSettings tiffSettings = new TiffSettings();
                tiffSettings.Compression = CompressionType.LZW; // LZW compression

                // Create a TIFF device with desired resolution and settings
                TiffDevice tiffDevice = new TiffDevice(new Resolution(300), tiffSettings);
                tiffDevice.RenderingOptions = renderOptions; // apply fallback font

                // Build output file name for the current page
                string outputTiffPath = Path.Combine(outputFolder, $"Page_{pageNumber}.tiff");

                // Render the page to TIFF
                tiffDevice.Process(pdfDoc.Pages[pageNumber], outputTiffPath);
            }
        }

        Console.WriteLine("PDF pages have been rendered to TIFF with LZW compression.");
    }
}
