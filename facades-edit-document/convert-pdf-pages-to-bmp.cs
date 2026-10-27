using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "BmpPages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Document implements IDisposable – use using for proper disposal
            using (Document doc = new Document(pdfPath))
            {
                // BmpDevice creates 24‑bit BMP images. Resolution can be adjusted as needed.
                var resolution = new Resolution(150); // 150 DPI – change if required
                var bmpDevice = new BmpDevice(resolution);

                // Pages are 1‑based (global rule)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    string outPath = Path.Combine(outputDir, $"Page_{pageNum}.bmp");
                    using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        // Convert the current page to BMP and write directly to the file stream
                        bmpDevice.Process(doc.Pages[pageNum], fs);
                    }
                    Console.WriteLine($"Saved page {pageNum} as BMP → {outPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
