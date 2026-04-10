using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;   // PngDevice resides here

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
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
            // Load the PDF document (lifecycle rule: use Document constructor)
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Prepare a PNG device with desired resolution
                var pngDevice = new PngDevice(new Resolution(300));

                // Set RenderingOptions and specify the default font name
                pngDevice.RenderingOptions = new RenderingOptions
                {
                    DefaultFontName = "Times New Roman"
                };

                // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    string outPath = Path.Combine(outputDir, $"page_{pageNum}.png");

                    // Convert the current page to PNG
                    pngDevice.Process(pdfDoc.Pages[pageNum], outPath);

                    Console.WriteLine($"Saved page {pageNum} → {outPath}");
                }
            }

            Console.WriteLine("All pages have been converted to images.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
