using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Apply Helvetica → Arial substitution using SimpleFontSubstitution
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Arial"));

            // Export pages 5‑7 as BMP images
            int startPage = 5;
            int endPage = 7;
            int resolutionDpi = 300; // DPI

            // BmpDevice expects a Resolution object
            BmpDevice bmpDevice = new BmpDevice(new Resolution(resolutionDpi));

            for (int i = startPage; i <= endPage && i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                string outputFile = $"page{i}.bmp";
                bmpDevice.Process(page, outputFile);
                Console.WriteLine($"Saved page {i} as {outputFile}");
            }
        }
    }
}
