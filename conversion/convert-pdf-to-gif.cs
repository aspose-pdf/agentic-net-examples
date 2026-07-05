using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory containing the PDF and where GIFs will be saved
        const string dataDir = @"YOUR_DATA_DIRECTORY";
        // PDF file name
        const string pdfFile = @"YOUR_PDF_FILE";

        string pdfPath = Path.Combine(dataDir, pdfFile);
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load PDF document (wrapped in using for deterministic disposal)
        using (Document pdfDocument = new Document(pdfPath))
        {
            // GifDevice with default resolution (no parameters)
            GifDevice gifDevice = new GifDevice();

            // Iterate through all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outPath = Path.Combine(dataDir, $"image{pageNumber}_out.gif");
                // Create output stream for each GIF image
                using (FileStream gifStream = new FileStream(outPath, FileMode.Create))
                {
                    // Convert the current page to GIF and write to the stream
                    gifDevice.Process(pdfDocument.Pages[pageNumber], gifStream);
                }
            }
        }

        Console.WriteLine("PDF successfully converted to GIF images.");
    }
}