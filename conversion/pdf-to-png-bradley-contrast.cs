using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // PngDevice, TiffDevice, Resolution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "output_images";
        const double threshold = 0.5; // value between 0.0 and 1.0

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create devices (they do NOT implement IDisposable, so no using block)
            PngDevice pngDevice = new PngDevice(new Resolution(300));
            TiffDevice tiffDevice = new TiffDevice();

            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                // Convert the current page to a PNG image in memory
                using (MemoryStream pngStream = new MemoryStream())
                {
                    pngDevice.Process(pdfDoc.Pages[pageNum], pngStream);
                    pngStream.Position = 0;

                    // Apply Bradley binarization to enhance contrast
                    using (MemoryStream binarizedStream = new MemoryStream())
                    {
                        tiffDevice.BinarizeBradley(pngStream, binarizedStream, threshold);
                        binarizedStream.Position = 0;

                        // Save the binarized image to disk (kept as PNG extension for consistency)
                        string outPath = Path.Combine(outputDir, $"page_{pageNum}_bradley.png");
                        File.WriteAllBytes(outPath, binarizedStream.ToArray());
                        Console.WriteLine($"Saved: {outPath}");
                    }
                }
            }
        }
    }
}
