using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "PagesPng";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Load PDF with deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create a concrete PNG device (devices are not IDisposable, so no using block)
            var pngDevice = new PngDevice(new Resolution(300))
            {
                TransparentBackground = true
            };

            // Pages are 1‑based
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];

                // Preserve original dimensions by ensuring MediaBox matches the page size
                page.MediaBox = new Rectangle(0, 0, page.PageInfo.Width, page.PageInfo.Height);

                // Convert page to PNG
                string outPath = Path.Combine(outputFolder, $"Page_{i}.png");
                using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    pngDevice.Process(page, outStream);
                }

                Console.WriteLine($"Saved page {i} as PNG.");
            }
        }
    }
}
