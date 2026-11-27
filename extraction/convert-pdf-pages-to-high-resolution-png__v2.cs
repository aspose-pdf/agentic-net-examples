using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "PngPages";
        const int resolutionDpi = 300; // high‑resolution output

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf rule)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                // Create a PNG device with the desired resolution.
                // PngDevice does NOT implement IDisposable, so do NOT wrap it in a using statement.
                var pngDevice = new PngDevice(new Resolution(resolutionDpi))
                {
                    // Set transparent background for PDFs that contain transparency.
                    TransparentBackground = true
                };

                // Define the output file name for the current page
                string outputPngPath = Path.Combine(outputFolder, $"Page_{pageNumber}.png");

                // Render the page onto a bitmap and save as PNG
                using (FileStream outStream = new FileStream(outputPngPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDoc.Pages[pageNumber], outStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as high‑resolution PNG: {outputPngPath}");
            }
        }
    }
}
