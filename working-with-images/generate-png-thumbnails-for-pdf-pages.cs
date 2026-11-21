using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "Thumbnails";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF inside a using block (document-disposal-with-using rule)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate pages using 1‑based indexing (page-indexing-one-based rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Desired thumbnail size in pixels
                const int targetWidth  = 150;
                const int targetHeight = 200;

                // Page size in points (1 point = 1/72 inch)
                double pageWidthPt  = page.PageInfo.Width;
                double pageHeightPt = page.PageInfo.Height;

                // Compute DPI needed to fit the page into the target dimensions
                // DPI = (targetPixels * 72) / pageSizeInPoints
                double dpiX = (targetWidth  * 72) / pageWidthPt;
                double dpiY = (targetHeight * 72) / pageHeightPt;
                double dpi  = Math.Min(dpiX, dpiY); // preserve aspect ratio

                // Cast DPI to int because Resolution ctor expects integers
                int intDpi = (int)Math.Round(dpi);

                // Create a PNG device with the calculated resolution.
                // PngDevice does NOT implement IDisposable, so we do NOT wrap it in a using block.
                var pngDevice = new PngDevice(new Resolution(intDpi, intDpi))
                {
                    TransparentBackground = true // optional, makes background transparent
                };

                string outPath = Path.Combine(outputDir, $"page_{i}.png");

                // Save the rendered page as PNG (SaveFormat.Png is implicit via PngDevice)
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    pngDevice.Process(page, outStream);
                }

                Console.WriteLine($"Thumbnail saved: {outPath}");
            }
        }
    }
}
