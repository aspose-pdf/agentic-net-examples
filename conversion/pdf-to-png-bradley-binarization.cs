using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "output_images";
        const double bradleyThreshold = 0.5; // value between 0.0 and 1.0

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle rule: using block)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create a PNG device with desired resolution
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution);

            // TiffDevice provides the Bradley binarization method
            TiffDevice tiffDevice = new TiffDevice();

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                // Convert the current page to a PNG image in memory
                using (MemoryStream pngStream = new MemoryStream())
                {
                    pngDevice.Process(pdfDoc.Pages[pageNum], pngStream);
                    pngStream.Position = 0; // reset stream for reading

                    // Apply Bradley binarization and write the result to a file
                    string outPath = Path.Combine(outputDir, $"page_{pageNum}_bradley.png");
                    using (FileStream outFile = new FileStream(outPath, FileMode.Create))
                    {
                        tiffDevice.BinarizeBradley(pngStream, outFile, bradleyThreshold);
                    }
                }
            }
        }

        Console.WriteLine("PDF to PNG conversion with Bradley binarization completed.");
    }
}