using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory containing the PDF file
        string dataDir = @"YOUR_DATA_DIRECTORY";
        // PDF file name
        string pdfFile = @"YOUR_PDF_FILE";

        string inputPath = Path.Combine(dataDir, pdfFile);
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document pdfDocument = new Document(inputPath))
        {
            // Create a Resolution object for 300 DPI
            Resolution resolution = new Resolution(300);

            // Initialize BmpDevice with the desired resolution
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // Optional: set high‑quality rendering options
            bmpDevice.RenderingOptions = new RenderingOptions
            {
                // Example settings (adjust if needed)
                // AntiAliasing = true,
                // TextAntiAliasing = true
            };

            // Iterate through all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Output BMP file path
                string outPath = Path.Combine(dataDir, $"image{pageNumber}_out.bmp");

                // Create the output stream (lifecycle rule: use using)
                using (FileStream bmpStream = new FileStream(outPath, FileMode.Create))
                {
                    // Convert the page to BMP and write to the stream
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }
            }
        }

        Console.WriteLine("PDF pages exported as BMP images at 300 DPI.");
    }
}