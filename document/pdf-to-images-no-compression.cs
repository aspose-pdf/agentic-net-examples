using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputFolder = "Images";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        using (Document doc = new Document(inputPath))
        {
            // Create a PNG device with a desired resolution (e.g., 300 DPI)
            var pngDevice = new PngDevice(new Resolution(300));
            // NOTE: Aspose.Pdf's PngDevice does not expose a CompressionLevel property.
            // PNG output is loss‑less; the library does not provide a way to turn off
            // compression via a property. The default compression is already minimal
            // and preserves the original visual fidelity.

            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                string outPath = Path.Combine(outputFolder, $"page_{i}.png");
                // Process the specific page and save it as PNG
                pngDevice.Process(doc.Pages[i], outPath);
                Console.WriteLine($"Saved page {i} to {outPath}");
            }
        }
    }
}
