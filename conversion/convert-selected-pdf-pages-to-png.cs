using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal (document-disposal-with-using rule)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Pages are 1‑based (page-indexing-one-based rule)
            int[] pagesToExport = { 3, 6, 9 };

            // PngDevice does not implement IDisposable, so instantiate it without a using block.
            var pngDevice = new PngDevice(100, 0); // width 100px, height 0 to preserve aspect ratio

            foreach (int pageNumber in pagesToExport)
            {
                if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                {
                    Console.WriteLine($"Page {pageNumber} is out of range. Skipping.");
                    continue;
                }

                Page page = pdfDoc.Pages[pageNumber];
                string outputPngPath = $"page_{pageNumber}.png";

                // Save the page image to a file stream.
                using (FileStream outStream = new FileStream(outputPngPath, FileMode.Create, FileAccess.Write))
                {
                    pngDevice.Process(page, outStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as PNG: {outputPngPath}");
            }
        }
    }
}
