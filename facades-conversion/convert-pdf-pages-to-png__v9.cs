using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "Images";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Set the desired resolution (DPI) for the PNG images
            var resolution = new Resolution(150);

            // PngDevice does NOT implement IDisposable, so we instantiate it once and reuse it
            var pngDevice = new PngDevice(resolution);

            // Iterate over each page (Aspose.Pdf pages are 1‑based)
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                string outPath = System.IO.Path.Combine(outputDir, $"page_{pageNumber}.png");
                using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    // Convert the page to PNG and write directly to the file stream
                    pngDevice.Process(doc.Pages[pageNumber], outStream);
                }
                Console.WriteLine($"Saved page {pageNumber} → {outPath}");
            }
        }
    }
}