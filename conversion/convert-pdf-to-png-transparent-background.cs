using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "PngPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                // Instantiate PngDevice directly (it does not implement IDisposable)
                var pngDevice = new PngDevice(new Resolution(300))
                {
                    TransparentBackground = true
                };

                string outputPath = Path.Combine(outputDir, $"Page_{pageNumber}.png");

                // Dispose only the stream; the device does not need disposal
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    pngDevice.Process(pdfDoc.Pages[pageNumber], outStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as PNG with transparent background.");
            }
        }
    }
}
