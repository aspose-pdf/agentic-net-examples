using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory that contains the source PDF and will receive the BMP files
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Name of the PDF file to convert
        string pdfFile = "YOUR_PDF_FILE";

        // Full path to the source PDF
        string inputPath = Path.Combine(dataDir, pdfFile);

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"PDF file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPath))
        {
            // Desired resolution (dots per inch). Higher DPI yields larger BMP files.
            // 300 DPI is a common choice for high‑quality 24‑bit BMP output.
            Resolution resolution = new Resolution(300);

            // Create a BMP device that will rasterize pages using the specified resolution
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // Aspose.Pdf uses 1‑based page indexing
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build the output file name for each page
                string outputPath = Path.Combine(dataDir, $"image{pageNumber}_out.bmp");

                // Write the BMP image to a file stream
                using (FileStream bmpStream = new FileStream(outputPath, FileMode.Create))
                {
                    // Convert the current page to BMP and save it into the stream
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as BMP: {outputPath}");
            }
        }
    }
}