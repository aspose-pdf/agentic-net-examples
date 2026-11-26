using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "EvenPageGifs";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // GifDevice is a concrete image device; it does NOT implement IDisposable.
            var gifDevice = new GifDevice(new Resolution(300));

            // Aspose.Pdf uses 1‑based page indexing
            for (int pageNumber = 2; pageNumber <= pdfDoc.Pages.Count; pageNumber += 2)
            {
                // Retrieve the even‑numbered page
                Page page = pdfDoc.Pages[pageNumber];

                // Build the output file name
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.gif");

                // Render the page to a GIF using a FileStream
                using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    gifDevice.Process(page, fs);
                }

                Console.WriteLine($"Saved even page {pageNumber} as GIF: {outputPath}");
            }
        }

        Console.WriteLine("All even‑numbered pages have been exported as GIF images.");
    }
}
