using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory containing the PDF and where BMP images will be saved
        string dataDir = @"YOUR_DATA_DIRECTORY";
        // PDF file name
        string pdfFile = @"YOUR_PDF_FILE";

        string inputPath = Path.Combine(dataDir, pdfFile);
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document pdfDocument = new Document(inputPath))
        {
            // BmpDevice with default settings (default resolution = 150 DPI)
            BmpDevice bmpDevice = new BmpDevice();

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(dataDir, $"image{pageNumber}_out.bmp");

                // Create a file stream for the BMP output (wrapped in using)
                using (FileStream bmpStream = new FileStream(outputPath, FileMode.Create))
                {
                    // Convert the current page to BMP and write to the stream
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as BMP: {outputPath}");
            }
        }
    }
}