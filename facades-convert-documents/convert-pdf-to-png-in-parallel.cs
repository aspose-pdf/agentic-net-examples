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

        // Load the PDF document (lifecycle: using for disposal)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Set the desired resolution for PNG output (higher = larger files, slower conversion)
            Resolution resolution = new Resolution(300);

            // Process each page in parallel to speed up conversion
            Parallel.For(1, pdfDoc.Pages.Count + 1, pageNumber =>
            {
                // Each thread creates its own PngDevice instance (thread‑safe)
                PngDevice pngDevice = new PngDevice(resolution);

                // Build the output file name for the current page
                string outPath = Path.Combine(outputDir, $"page_{pageNumber}.png");

                // Convert the page to PNG and write to the file
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDoc.Pages[pageNumber], outStream);
                }

                Console.WriteLine($"Saved {outPath}");
            });
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}