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

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                // BmpDevice with default settings:
                // - page number is supplied via the Process method, not the constructor
                // - default resolution is 300 DPI when not specified
                BmpDevice bmpDevice = new BmpDevice();

                // Build output file name for each page
                string outputPath = Path.Combine(outputFolder, $"Page_{pageNum}.bmp");

                // Convert the current page to BMP
                bmpDevice.Process(pdfDoc.Pages[pageNum], outputPath);
            }
        }

        Console.WriteLine($"PDF pages have been converted to BMP images in '{outputFolder}'.");
    }
}