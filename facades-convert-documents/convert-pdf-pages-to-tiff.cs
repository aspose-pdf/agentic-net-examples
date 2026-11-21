using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "TiffPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load PDF document (IDisposable)
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Define the resolution for the TIFF images (e.g., 300 DPI)
                var resolution = new Resolution(300);

                // TiffDevice does NOT implement IDisposable – instantiate it once and reuse.
                var tiffDevice = new TiffDevice(resolution);

                // Iterate over each page (Aspose.Pdf pages are 1‑based)
                for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
                {
                    // Convert the page to a TIFF image stored in a memory stream
                    using (var ms = new MemoryStream())
                    {
                        tiffDevice.Process(pdfDoc.Pages[pageIndex], ms);
                        ms.Position = 0; // rewind

                        string outPath = Path.Combine(outputDir, $"page_{pageIndex}.tiff");
                        File.WriteAllBytes(outPath, ms.ToArray());
                        Console.WriteLine($"Saved page {pageIndex} as TIFF: {outPath}");
                    }
                }
            }
        }
        // The TiffDevice works on all platforms; no GDI+ dependency.
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
