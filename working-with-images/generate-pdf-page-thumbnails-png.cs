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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create a ThumbnailDevice with the required dimensions (150x200 pixels)
            Aspose.Pdf.Devices.ThumbnailDevice thumbDevice = new Aspose.Pdf.Devices.ThumbnailDevice(150, 200);

            // Pages are 1‑based indexed in Aspose.Pdf
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNum}.png");

                // Write each thumbnail to a separate PNG file
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    thumbDevice.Process(pdfDoc.Pages[pageNum], outStream);
                }
            }
        }

        Console.WriteLine("Thumbnail images have been generated successfully.");
    }
}