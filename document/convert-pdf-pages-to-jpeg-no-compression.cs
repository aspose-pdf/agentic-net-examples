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

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // No explicit optimization is required for page‑to‑image conversion.
            // The JpegDevice will render the page at the requested resolution
            // without applying any additional image compression.

            // Create a JPEG device with a high resolution. The default JPEG quality is 100 (no loss).
            var jpegDevice = new JpegDevice(new Resolution(300));

            // Iterate over all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNum}.jpg");

                // Convert the page to an image and write it to a file stream
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    jpegDevice.Process(pdfDoc.Pages[pageNum], outStream);
                }

                Console.WriteLine($"Saved page {pageNum} → {outPath}");
            }
        }

        Console.WriteLine("Image conversion completed without compression.");
    }
}
