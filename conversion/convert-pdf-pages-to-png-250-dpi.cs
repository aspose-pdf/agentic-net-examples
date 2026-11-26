using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputFolder = "Pages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Load PDF with deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            int startPage = 5;
            int endPage = 7;

            // Adjust range if document has fewer pages
            if (endPage > pdfDoc.Pages.Count)
            {
                Console.Error.WriteLine($"Document has only {pdfDoc.Pages.Count} pages. Adjusting range.");
                endPage = pdfDoc.Pages.Count;
            }

            // PNG device at 250 DPI – use Resolution object as required by the API
            var pngDevice = new PngDevice(new Resolution(250));

            for (int i = startPage; i <= endPage; i++)
            {
                // Pages are 1‑based
                Page page = pdfDoc.Pages[i];

                string outPath = Path.Combine(outputFolder, $"Page_{i}.png");
                using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    pngDevice.Process(page, outStream);
                }

                Console.WriteLine($"Saved page {i} as PNG → {outPath}");
            }
        }
    }
}
