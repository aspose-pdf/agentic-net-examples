using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;   // BmpDevice and Resolution are defined here

class ExportPdfPagesToBmp
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputFolder  = "BmpPages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Set rendering resolution to 300 DPI for high‑quality output
            Resolution renderResolution = new Resolution(300);

            // Iterate pages using 1‑based indexing (Aspose.Pdf convention)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"Page_{pageNumber}.bmp");

                // Create a file stream for the BMP image
                using (FileStream bmpStream = new FileStream(outputPath, FileMode.Create))
                {
                    // BmpDevice renders a page to BMP using the specified resolution
                    BmpDevice bmpDevice = new BmpDevice(renderResolution);
                    bmpDevice.Process(pdfDoc.Pages[pageNumber], bmpStream);
                }

                Console.WriteLine($"Exported page {pageNumber} to {outputPath}");
            }
        }

        Console.WriteLine("All pages have been exported as BMP images at 300 DPI.");
    }
}