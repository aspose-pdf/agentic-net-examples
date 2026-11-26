using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "PagesAsPng";

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
            // Devices (e.g., PngDevice) are concrete but do NOT implement IDisposable.
            // Create a single PNG device with 150 DPI resolution.
            var pngDevice = new PngDevice(new Resolution(150));

            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                string outPath = Path.Combine(outputFolder, $"Page_{i}.png");
                // Render the current page to PNG and save it
                pngDevice.Process(pdfDoc.Pages[i], outPath);
            }
        }

        Console.WriteLine($"All pages have been saved as PNG files in '{outputFolder}'.");
    }
}
