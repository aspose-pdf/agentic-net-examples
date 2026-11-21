using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExportedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // JpegDevice with 300 DPI resolution and quality set to 85
            // Devices are concrete, not disposable, so we instantiate once and reuse.
            JpegDevice jpegDevice = new JpegDevice(300, 85);

            // Pages are 1‑based in Aspose.Pdf
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Convert the current page to a JPEG image
                using (MemoryStream imgStream = new MemoryStream())
                {
                    jpegDevice.Process(doc.Pages[pageNum], imgStream);
                    string outPath = Path.Combine(outputDir, $"Page_{pageNum}.jpg");
                    File.WriteAllBytes(outPath, imgStream.ToArray());
                    Console.WriteLine($"Saved page {pageNum} as JPEG to {outPath}");
                }
            }
        }
    }
}
