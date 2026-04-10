using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // PngDevice resides here

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure PNG device with desired resolution
                var resolution = new Resolution(300);
                // PngDevice does NOT implement IDisposable, so instantiate without a using block
                var pngDevice = new PngDevice(resolution);

                // Set rendering options to use Arial as the fallback font
                pngDevice.RenderingOptions = new RenderingOptions
                {
                    DefaultFontName = "Arial"
                };

                // Render each page individually
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    string outPath = Path.Combine(outputDir, $"page_{pageNum}.png");
                    try
                    {
                        pngDevice.Process(pdfDoc.Pages[pageNum], outPath);
                        Console.WriteLine($"Saved page {pageNum} → {outPath}");
                    }
                    catch (TypeInitializationException)
                    {
                        Console.WriteLine("Image conversion requires Windows GDI+. Skipping remaining pages.");
                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
