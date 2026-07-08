using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // PngDevice and Resolution are defined here

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "Thumbnails";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (lifecycle: using block ensures disposal)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Define a high resolution (e.g., 300 DPI) for thumbnail quality
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution);

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string outPath = Path.Combine(outputFolder, $"page_{pageNum}.png");

                // Convert the current page to PNG and write to a file stream
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDoc.Pages[pageNum], outStream);
                }
            }
        }

        Console.WriteLine("Thumbnail images have been created successfully.");
    }
}