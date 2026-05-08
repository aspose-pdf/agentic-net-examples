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

        string pdfPath = Path.Combine(dataDir, pdfFile);
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document pdfDocument = new Document(pdfPath))
        {
            // GifDevice with default parameters (default resolution)
            GifDevice gifDevice = new GifDevice();

            // Iterate through all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Output GIF file path
                string outPath = Path.Combine(dataDir, $"image{pageNumber}_out.gif");

                // Create a file stream for the GIF image
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