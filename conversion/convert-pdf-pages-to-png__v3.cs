using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "PageImages";
        const int dpi = 300; // Desired resolution

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Pages are 1‑based in Aspose.Pdf
                for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
                {
                    string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");

                    // Each image is written to its own file stream
                    using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
                    {
                        // PngDevice performs rasterization; Resolution sets DPI
                        Resolution resolution = new Resolution(dpi);
                        PngDevice pngDevice = new PngDevice(resolution);

                        // Convert the specific page to PNG
                        pngDevice.Process(pdfDoc.Pages[pageNumber], imageStream);
                    }

                    Console.WriteLine($"Saved page {pageNumber} as PNG → {outputPath}");
                }
            }
        }
        catch (TypeInitializationException)
        {
            // PNG conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("PNG conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}