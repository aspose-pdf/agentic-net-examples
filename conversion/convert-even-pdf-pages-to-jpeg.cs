using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputFolder = "OutputImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // JpegDevice with 300 DPI (devices are not IDisposable, so no using block)
            JpegDevice jpegDevice = new JpegDevice(300);

            // Aspose.Pdf uses 1‑based page indexing.
            // Iterate over even‑numbered pages: 2, 4, 6, ...
            for (int pageNum = 2; pageNum <= pdfDoc.Pages.Count; pageNum += 2)
            {
                Page page = pdfDoc.Pages[pageNum];

                // Build the output JPEG file name
                string outputPath = Path.Combine(outputFolder, $"page_{pageNum}.jpg");

                // Write the page image to a file stream
                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    jpegDevice.Process(page, imageStream);
                }

                Console.WriteLine($"Saved even page {pageNum} as JPEG to '{outputPath}'.");
            }
        }
    }
}
