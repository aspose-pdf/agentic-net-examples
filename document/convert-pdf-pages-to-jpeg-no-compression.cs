using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "Images";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Wrap the Document in a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];

                // PngDevice does not expose a CompressionLevel property.
                // By not setting any compression‑related options we keep the default
                // behavior, which preserves the original image data as closely as possible.
                PngDevice pngDevice = new PngDevice();

                string outPath = Path.Combine(outputDir, $"Page_{pageNum}.png");

                // Render the page to a PNG file
                using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    pngDevice.Process(page, outStream);
                }

                Console.WriteLine($"Saved page {pageNum} as PNG: {outPath}");
            }
        }
    }
}