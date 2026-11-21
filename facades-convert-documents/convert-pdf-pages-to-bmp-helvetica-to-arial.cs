using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text; // <-- needed for SimpleFontSubstitution and FontRepository

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "Images";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Apply Helvetica → Arial substitution for the whole document
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Arial"));

        using (Document doc = new Document(inputPath))
        {
            // Define the resolution for the bitmap images (e.g., 300 DPI)
            var resolution = new Resolution(300);

            // BmpDevice is a concrete device and does NOT implement IDisposable, so do NOT wrap it in a using block.
            var bmpDevice = new BmpDevice(resolution);

            for (int pageNum = 5; pageNum <= 7; pageNum++)
            {
                if (pageNum > doc.Pages.Count)
                {
                    Console.WriteLine($"Page {pageNum} does not exist in the document.");
                    continue;
                }

                string outPath = Path.Combine(outputDir, $"Page_{pageNum}.bmp");

                // Render the page to BMP using BmpDevice
                bmpDevice.Process(doc.Pages[pageNum], outPath);

                Console.WriteLine($"Saved page {pageNum} as BMP to {outPath}");
            }
        }
    }
}
