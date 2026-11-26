using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "GifPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                // Preserve original page size by using a 72 DPI resolution.
                // At 72 DPI, one PDF point (1/72 inch) maps to one pixel, so the
                // rendered image keeps the exact width/height of the page.
                var resolution = new Resolution(72);
                var gifDevice = new GifDevice(resolution);

                string outPath = Path.Combine(outputFolder, $"page_{i}.gif");
                using (FileStream imageStream = new FileStream(outPath, FileMode.Create))
                {
                    gifDevice.Process(pdfDoc.Pages[i], imageStream);
                }

                Console.WriteLine($"Saved page {i} as GIF → {outPath}");
            }
        }
    }
}
