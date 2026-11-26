using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "BmpPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // BmpDevice requires a Resolution object; devices are not IDisposable
            BmpDevice bmpDevice = new BmpDevice(new Resolution(200));

            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                Page page = pdfDoc.Pages[pageNumber];
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.bmp");

                // Save the page as a BMP image; only the stream needs disposal
                using (FileStream bmpStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    bmpDevice.Process(page, bmpStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as BMP: {outputPath}");
            }
        }

        Console.WriteLine("PDF to BMP conversion completed.");
    }
}
