using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class ExportPdfToBmp
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDir = "BmpPages";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Create a minimal PDF if it does not already exist (prevents FileNotFoundException)
        if (!File.Exists(inputPdfPath))
        {
            using (Document seed = new Document())
            {
                seed.Pages.Add(); // add a blank page
                seed.Save(inputPdfPath);
            }
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Set resolution to 300 DPI
            Resolution resolution = new Resolution(300);

            // Initialise BmpDevice with the desired resolution and optional high‑quality options
            BmpDevice bmpDevice = new BmpDevice(resolution)
            {
                RenderingOptions = new RenderingOptions
                {
                    // Uncomment if the version supports it:
                    // AntiAliasing = true
                }
            };

            // Iterate through all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string bmpPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");
                using (FileStream bmpStream = new FileStream(bmpPath, FileMode.Create))
                {
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }
                Console.WriteLine($"Page {pageNumber} saved as BMP: {bmpPath}");
            }
        }
    }
}
