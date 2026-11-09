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

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document (Document implements IDisposable, so we use a using block)
            using (Document pdfDocument = new Document(inputPdf))
            {
                // Define the resolution (DPI) for the output BMP images.
                var resolution = new Resolution(150);

                // BmpDevice renders each page to a 24‑bit BMP by default.
                // NOTE: Device classes are NOT IDisposable, therefore they must NOT be placed in a using statement.
                BmpDevice bmpDevice = new BmpDevice(resolution);

                for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
                {
                    string outputPath = Path.Combine(outputDir, $"Page_{pageNumber}.bmp");

                    // Render the current page to a BMP file.
                    using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        bmpDevice.Process(pdfDocument.Pages[pageNumber], fs);
                    }

                    Console.WriteLine($"Saved page {pageNumber} as BMP → {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
