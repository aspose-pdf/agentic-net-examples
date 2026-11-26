using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "GifPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // GifDevice expects a Resolution object, not an int. Devices are not IDisposable.
            var gifDevice = new GifDevice(new Resolution(100));

            // Aspose.Pdf uses 1‑based page indexing
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string outPath = Path.Combine(outputDir, $"Page_{pageNum}.gif");

                // Create a stream for the GIF output
                using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    // Convert the current page to a GIF image
                    gifDevice.Process(pdfDoc.Pages[pageNum], outStream);
                }

                Console.WriteLine($"Saved page {pageNum} as GIF → {outPath}");
            }
        }
    }
}
