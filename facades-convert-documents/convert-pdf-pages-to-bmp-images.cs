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
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document (Document implements IDisposable)
            using (Document doc = new Document(pdfPath))
            {
                // Set the desired resolution for the BMP output
                Resolution resolution = new Resolution(150);

                // BmpDevice is a concrete image device but it does NOT implement IDisposable.
                // Therefore we instantiate it without a using‑statement.
                BmpDevice bmpDevice = new BmpDevice(resolution);

                // Convert pages 2 through 6 (or up to the last page if the PDF is shorter)
                for (int pageNumber = 2; pageNumber <= 6 && pageNumber <= doc.Pages.Count; pageNumber++)
                {
                    string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.bmp");
                    // The device works on a Page object, not directly on the Document.
                    bmpDevice.Process(doc.Pages[pageNumber], outPath);
                    Console.WriteLine($"Saved page {pageNumber} as BMP → {outPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
