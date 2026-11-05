using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class PdfToTiffConverter
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";
        // Folder where TIFF images will be saved
        const string outputFolder = "TiffPages";
        // Desired resolution in DPI
        const int dpi = 300;

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Create a TiffDevice with the required DPI. TiffDevice does NOT implement IDisposable,
                // so we instantiate it without a using statement.
                var tiffDevice = new TiffDevice(new Resolution(dpi));

                // Aspose.Pdf uses 1‑based page indexing
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    // Build output file name for each page
                    string tiffPath = Path.Combine(outputFolder, $"page_{pageNum}.tiff");

                    // Convert the current page to a TIFF file
                    tiffDevice.Process(pdfDoc.Pages[pageNum], tiffPath);

                    Console.WriteLine($"Saved page {pageNum} as TIFF → {tiffPath}");
                }
            }

            Console.WriteLine("All pages converted successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
