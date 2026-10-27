using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "output_images";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Set the desired resolution (72 DPI) for the PNG output
            var resolution = new Resolution(72);

            // Iterate over all pages (1‑based indexing) and save each as a PNG file
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNumber}.png");

                // Use PngDevice (cross‑platform) to render the page to a PNG stream
                using (FileStream imageStream = new FileStream(outPath, FileMode.Create))
                {
                    var pngDevice = new PngDevice(resolution);
                    pngDevice.Process(doc.Pages[pageNumber], imageStream);
                }
            }
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}
