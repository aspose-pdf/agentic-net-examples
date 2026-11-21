using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "output_images";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        int totalPages;
        // Get page count (lifecycle rule – use using block)
        using (Document doc = new Document(inputPdfPath))
        {
            totalPages = doc.Pages.Count;
        }

        // Process each page in parallel
        Parallel.For(1, totalPages + 1, pageNumber =>
        {
            // Each thread works with its own Document instance (Pdf objects are not thread‑safe)
            using (Document doc = new Document(inputPdfPath))
            {
                // Prepare PNG device – devices are NOT IDisposable, so no using block
                var resolution = new Resolution(150);
                var pngDevice = new PngDevice(resolution) { TransparentBackground = true };

                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");
                using (var outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    // Convert the specific page to PNG
                    pngDevice.Process(doc.Pages[pageNumber], outStream);
                }
            }
        });

        Console.WriteLine($"Conversion completed. PNG files are saved in '{outputFolder}'.");
    }
}
