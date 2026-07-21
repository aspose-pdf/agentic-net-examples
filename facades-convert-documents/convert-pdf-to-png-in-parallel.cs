using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle: using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Define the resolution for PNG output (higher = larger files, slower conversion)
            Resolution resolution = new Resolution(300);

            // Process pages in parallel (Aspose.Pdf uses 1‑based page indexing)
            Parallel.For(1, pdfDoc.Pages.Count + 1, pageNumber =>
            {
                // Each thread creates its own PngDevice instance (no shared state)
                PngDevice pngDevice = new PngDevice(resolution);

                string outPath = Path.Combine(outputDir, $"page_{pageNumber}.png");

                // Write the PNG image to a file stream (lifecycle: using)
                using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    // Convert the specific page to PNG
                    pngDevice.Process(pdfDoc.Pages[pageNumber], outStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as PNG.");
            });
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}
