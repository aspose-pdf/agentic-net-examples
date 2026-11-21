using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // PngDevice resides here

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "PngPages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Define the resolution (DPI) for the PNG output – optional but recommended
            var resolution = new Resolution(300);

            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                // PngDevice does NOT implement IDisposable, so do NOT wrap it in a using statement
                var pngDevice = new PngDevice(resolution)
                {
                    TransparentBackground = true // Enable transparent background
                };

                string pngPath = Path.Combine(outputDir, $"Page_{i}.png");

                // Convert the current page to PNG
                pngDevice.Process(pdfDoc.Pages[i], pngPath);
            }
        }

        Console.WriteLine("PDF pages have been converted to PNG with transparent background.");
    }
}
