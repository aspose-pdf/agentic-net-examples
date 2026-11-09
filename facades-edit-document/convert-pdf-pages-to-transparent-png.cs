using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "Pages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create a single PNG device (devices are not IDisposable) with required resolution
            var pngDevice = new PngDevice(new Resolution(150))
            {
                // Enable transparent background for PNG output
                TransparentBackground = true
            };

            // Iterate over each page and convert to PNG with transparent background at 150 DPI
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string outPath = Path.Combine(outputFolder, $"Page_{pageNum}.png");
                using (FileStream imageStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    // Process the page with the PNG device; Page has no Convert method
                    pngDevice.Process(pdfDoc.Pages[pageNum], imageStream);
                }

                Console.WriteLine($"Saved page {pageNum} as {outPath}");
            }
        }
    }
}
