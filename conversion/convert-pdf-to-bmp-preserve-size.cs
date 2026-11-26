using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "BmpPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Pages are 1‑based in Aspose.Pdf
                for (int i = 1; i <= pdfDoc.Pages.Count; i++)
                {
                    Page page = pdfDoc.Pages[i];

                    // Page dimensions are in points (1 point = 1/72 inch)
                    double pageWidth = page.PageInfo.Width;
                    double pageHeight = page.PageInfo.Height;

                    // Use 72 DPI so that 1 point maps to 1 pixel, preserving original size
                    int widthPx = (int)Math.Ceiling(pageWidth);
                    int heightPx = (int)Math.Ceiling(pageHeight);

                    // BmpDevice expects a Resolution object for the DPI argument
                    BmpDevice bmpDevice = new BmpDevice(widthPx, heightPx, new Resolution(72));

                    string outPath = Path.Combine(outputDir, $"Page_{i}.bmp");
                    using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        bmpDevice.Process(page, outStream);
                    }

                    Console.WriteLine($"Saved page {i} as BMP → {outPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
