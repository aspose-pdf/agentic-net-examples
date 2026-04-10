using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory containing the PDF file (ensure it exists)
        string dataDir = @"YOUR_DATA_DIRECTORY";
        Directory.CreateDirectory(dataDir);

        // PDF file name
        string pdfFile = @"YOUR_PDF_FILE";

        string inputPath = Path.Combine(dataDir, pdfFile);
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPath))
        {
            // BmpDevice with default resolution (no parameters needed)
            BmpDevice bmpDevice = new BmpDevice();

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Output BMP file path for the current page
                string outputPath = Path.Combine(dataDir, $"image{pageNumber}_out.bmp");

                // Create a FileStream inside a using block; it will be closed automatically
                using (FileStream bmpStream = new FileStream(outputPath, FileMode.Create))
                {
                    // Convert the page to BMP and write it to the stream
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }
            }
        }

        Console.WriteLine("PDF has been successfully converted to BMP images.");
    }
}
