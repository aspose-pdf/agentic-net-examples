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
            // Create a ThumbnailDevice with custom dimensions (e.g., 150x150 pixels)
            ThumbnailDevice thumbDevice = new ThumbnailDevice(150, 150);

            // Iterate through pages using 1‑based indexing (Aspose.Pdf convention)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string outPath = Path.Combine(outputDir, $"thumb_page{pageNum}.png");

                // Create the output file stream
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    // Convert the current page to a PNG thumbnail and write to the stream
                    thumbDevice.Process(pdfDoc.Pages[pageNum], outStream);
                }
            }
        }

        Console.WriteLine("Thumbnail images have been generated successfully.");
    }
}