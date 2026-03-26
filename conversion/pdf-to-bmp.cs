using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory containing the PDF file
        string dataDir = @"YOUR_DATA_DIRECTORY";
        // PDF file name
        string pdfFile = @"YOUR_PDF_FILE";

        string inputPath = Path.Combine(dataDir, pdfFile);
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF document (wrapped in using for deterministic disposal)
        using (Document pdfDocument = new Document(inputPath))
        {
            // BmpDevice with default resolution (150 DPI)
            BmpDevice bmpDevice = new BmpDevice();

            for (int pageNum = 1; pageNum <= pdfDocument.Pages.Count; pageNum++)
            {
                string outPath = Path.Combine(dataDir, $"image{pageNum}_out.bmp");
                using (FileStream bmpStream = new FileStream(outPath, FileMode.Create))
                {
                    // Convert the page to BMP and write to the stream
                    bmpDevice.Process(pdfDocument.Pages[pageNum], bmpStream);
                }
            }
        }

        Console.WriteLine("PDF pages have been converted to BMP images.");
    }
}