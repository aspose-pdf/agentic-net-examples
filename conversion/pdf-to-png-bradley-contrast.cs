using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "output_png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // PNG conversion device with desired resolution
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution);

            // Device used for Bradley binarization
            TiffDevice tiffDevice = new TiffDevice();

            // Threshold for Bradley algorithm (0.0 to 1.0)
            double threshold = 0.5;

            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                // Convert current page to PNG in memory
                using (MemoryStream pngStream = new MemoryStream())
                {
                    pngDevice.Process(pdfDoc.Pages[pageNum], pngStream);
                    pngStream.Position = 0; // reset stream for reading

                    // Apply Bradley binarization
                    using (MemoryStream binarizedStream = new MemoryStream())
                    {
                        tiffDevice.BinarizeBradley(pngStream, binarizedStream, threshold);
                        binarizedStream.Position = 0; // reset for writing

                        // Save the binarized PNG to file
                        string outPath = Path.Combine(outputDir, $"page_{pageNum}.png");
                        using (FileStream fileOut = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                        {
                            binarizedStream.CopyTo(fileOut);
                        }
                    }
                }
            }
        }

        Console.WriteLine("Conversion completed: PDF pages saved as PNG with Bradley contrast enhancement.");
    }
}