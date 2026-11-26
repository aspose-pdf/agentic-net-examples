using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "EMF_Output";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];

                // Convert page size from points (1/72 inch) to pixels.
                // Using a DPI of 96 (screen DPI) keeps the visual size identical.
                const int dpi = 96;
                int width = (int)Math.Round(page.PageInfo.Width * dpi / 72);
                int height = (int)Math.Round(page.PageInfo.Height * dpi / 72);

                // EmfDevice does NOT implement IDisposable, so instantiate it directly.
                EmfDevice emfDevice = new EmfDevice(width, height);
                // Optional: set a higher resolution if required, e.g. emfDevice.Resolution = new Resolution(300);

                string outPath = Path.Combine(outputDir, $"Page_{i}.emf");
                emfDevice.Process(page, outPath);
            }
        }

        Console.WriteLine($"PDF pages have been exported to EMF files in '{outputDir}'.");
    }
}
