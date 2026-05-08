using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // ThumbnailDevice resides here

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "Thumbnails";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create a thumbnail device with custom size (e.g., 150x150 pixels)
            ThumbnailDevice thumbDevice = new ThumbnailDevice(150, 150);

            // Pages are 1‑based indexed in Aspose.Pdf
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];
                string outPath = Path.Combine(outputFolder, $"thumb_page{pageNum}.png");

                // Save each thumbnail to a PNG file via a FileStream
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    thumbDevice.Process(page, outStream);
                }
            }
        }

        Console.WriteLine("Thumbnail images have been generated.");
    }
}