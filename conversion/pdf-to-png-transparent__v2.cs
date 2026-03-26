using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "png_pages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        using (Document pdfDoc = new Document(inputPath))
        {
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                // PngDevice does not implement IDisposable, so do not wrap it in a using block.
                // Resolution is read‑only; provide it via the constructor.
                var pngDevice = new PngDevice(new Resolution(300))
                {
                    TransparentBackground = true
                };

                string outPath = Path.Combine(outputDir, $"page_{i}.png");
                using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    pngDevice.Process(pdfDoc.Pages[i], outStream);
                }
            }
        }

        Console.WriteLine("PDF pages have been converted to PNG with transparent background.");
    }
}