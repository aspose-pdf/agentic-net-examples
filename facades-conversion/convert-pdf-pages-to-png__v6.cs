using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Images";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (FormPresentationMode does not exist in this version)
        using (Document doc = new Document(inputPdf))
        {
            // Set up a PNG device with desired resolution
            var resolution = new Resolution(150); // 150 DPI – adjust as needed
            var pngDevice = new PngDevice(resolution);

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNum}.png");
                using (FileStream imageStream = new FileStream(outPath, FileMode.Create))
                {
                    pngDevice.Process(doc.Pages[pageNum], imageStream);
                }
                Console.WriteLine($"Saved page {pageNum} as PNG to {outPath}");
            }
        }
    }
}
