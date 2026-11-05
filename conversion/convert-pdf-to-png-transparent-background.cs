using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // PngDevice resides here

// Convert each page of a PDF to a separate PNG image with a transparent background.
class PdfToPngConverter
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string outFolder = "PngPages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                // Prepare output file path for the current page
                string pngPath = Path.Combine(outFolder, $"Page_{pageNum}.png");

                // Configure resolution (you can adjust DPI as needed)
                Resolution resolution = new Resolution(300); // 300 DPI for high quality

                // Configure PngDevice (concrete implementation of ImageDevice):
                // - Enable transparent background
                // - Use the resolution defined above
                PngDevice pngDevice = new PngDevice(resolution)
                {
                    TransparentBackground = true
                };

                // Convert the current page to PNG using the configured device and write directly to file
                pngDevice.Process(pdfDoc.Pages[pageNum], pngPath);

                Console.WriteLine($"Page {pageNum} saved as PNG: {pngPath}");
            }
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}
