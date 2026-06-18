using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory containing the source PDF and where BMP files will be written
        string dataDir = @"C:\Data\";               // <-- adjust as needed
        string pdfFileName = "input.pdf";

        string pdfPath = Path.Combine(dataDir, pdfFileName);
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document (Document implements IDisposable)
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Define the resolution for the output BMP images.
            // Higher DPI yields higher quality; 300 DPI is typical for 24‑bit BMP.
            Resolution resolution = new Resolution(300);

            // BmpDevice converts PDF pages to BMP format.
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // Aspose.Pdf uses 1‑based page indexing.
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build the output file name for each page.
                string bmpPath = Path.Combine(dataDir, $"page_{pageNumber}.bmp");

                // Convert the current page to BMP and write it to a file stream.
                using (FileStream bmpStream = new FileStream(bmpPath, FileMode.Create))
                {
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as BMP: {bmpPath}");
            }
        }
    }
}