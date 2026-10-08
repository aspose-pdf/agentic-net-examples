using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // <-- added for PngDevice and Resolution

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "Pages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF once; wrap in using for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            int pageCount = doc.Pages.Count;
            object sync = new object(); // synchronize access to the shared Document

            // Process pages in parallel to speed up conversion
            Parallel.For(1, pageCount + 1, pageNumber =>
            {
                // Create a PNG device with a desired resolution (e.g., 300 DPI)
                var resolution = new Resolution(300);
                var pngDevice = new PngDevice(resolution);

                string outPath = Path.Combine(outputDir, $"page_{pageNumber}.png");

                // Document and its Page objects are not thread‑safe; protect access with a lock
                Page page;
                lock (sync)
                {
                    page = doc.Pages[pageNumber];
                }

                // Render the page to PNG
                pngDevice.Process(page, outPath);

                Console.WriteLine($"Saved page {pageNumber} → {outPath}");
            });
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}
