using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "Thumbnails";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create a ThumbnailDevice with the required dimensions (150x200 pixels)
            ThumbnailDevice thumbDevice = new ThumbnailDevice(150, 200);

            // Pages are 1‑based indexed in Aspose.Pdf
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNum}.png");

                // Convert the page to a PNG thumbnail and write it to a file
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    thumbDevice.Process(pdfDoc.Pages[pageNum], outStream);
                }

                Console.WriteLine($"Thumbnail saved: {outPath}");
            }
        }
    }
}