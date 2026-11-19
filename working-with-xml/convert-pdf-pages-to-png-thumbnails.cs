using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Thumbnails";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create a PNG device with the desired high resolution (e.g., 300 DPI)
            var pngDevice = new PngDevice(new Resolution(300))
            {
                // Optional: make background transparent for thumbnail aesthetics
                TransparentBackground = true
            };

            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                // Build the output file name for the current page
                string outPath = Path.Combine(outputDir, $"Page_{pageNum}.png");

                // Convert the page to PNG and write directly to a file stream
                using (FileStream imageStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    pngDevice.Process(pdfDoc.Pages[pageNum], imageStream);
                }
            }
        }

        Console.WriteLine("Thumbnail images have been created successfully.");
    }
}
