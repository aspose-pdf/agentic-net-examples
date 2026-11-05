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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // BmpDevice does NOT implement IDisposable – instantiate directly
                BmpDevice bmpDevice = new BmpDevice();

                // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
                for (int i = 1; i <= pdfDoc.Pages.Count; i++)
                {
                    Page page = pdfDoc.Pages[i];
                    string outPath = Path.Combine(outputDir, $"page_{i}.bmp");

                    // Save each page as a BMP image
                    using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        bmpDevice.Process(page, outStream);
                    }

                    Console.WriteLine($"Saved page {i} as BMP → {outPath}");
                }
            }

            Console.WriteLine("PDF to BMP conversion completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
