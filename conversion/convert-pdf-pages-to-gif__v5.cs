using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "GifPages";

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
            // GifDevice is a concrete image device but does NOT implement IDisposable.
            // Create a single instance and reuse it for all pages.
            GifDevice gifDevice = new GifDevice(new Resolution(72));

            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                // Prepare the output file path for the current page
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.gif");

                // Only the stream needs to be disposed; the device does not.
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    gifDevice.Process(pdfDoc.Pages[pageNumber], outStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as GIF: {outputPath}");
            }
        }

        Console.WriteLine("All pages have been converted to GIF images.");
    }
}
