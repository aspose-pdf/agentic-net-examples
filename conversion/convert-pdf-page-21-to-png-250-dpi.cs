using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "page21.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify the document has at least 21 pages (1‑based indexing)
            if (doc.Pages.Count < 21)
            {
                Console.Error.WriteLine("The document contains fewer than 21 pages.");
                return;
            }

            // Retrieve the 21st page
            Page page = doc.Pages[21];

            // Create a PNG device with 250 DPI resolution
            Resolution resolution = new Resolution(250);
            PngDevice pngDevice = new PngDevice(resolution);

            // Render and save the page as PNG
            pngDevice.Process(page, outputPath);
        }

        Console.WriteLine($"Page 21 has been saved as PNG to '{outputPath}'.");
    }
}
