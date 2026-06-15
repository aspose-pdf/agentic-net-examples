using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory that contains the source PDF and where GIFs will be written
        const string dataDir = @"YOUR_DATA_DIRECTORY";
        // Name of the PDF file to convert
        const string pdfFile = "YOUR_PDF_FILE.pdf";

        string pdfPath = Path.Combine(dataDir, pdfFile);
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Initialize GifDevice with default resolution (no parameters)
            GifDevice gifDevice = new GifDevice();

            // Pages are 1‑based; iterate through each page
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build output file name for the current page
                string outputPath = Path.Combine(dataDir, $"image{pageNumber}_out.gif");

                // Create a file stream for the GIF image (using ensures proper closure)
                using (FileStream gifStream = new FileStream(outputPath, FileMode.Create))
                {
                    // Convert the page to GIF and write it to the stream
                    gifDevice.Process(pdfDocument.Pages[pageNumber], gifStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as GIF: {outputPath}");
            }
        }
    }
}