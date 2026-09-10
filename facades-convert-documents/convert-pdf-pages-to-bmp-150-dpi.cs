using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory containing the PDF and where BMP images will be saved
        const string dataDir = @"C:\PdfSamples";
        // Input PDF file name
        const string pdfFile = "sample.pdf";

        // Build full path to the input PDF
        string inputPath = Path.Combine(dataDir, pdfFile);

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPath))
        {
            // Create a Resolution object with 150 DPI (namespace Aspose.Pdf.Devices)
            Aspose.Pdf.Devices.Resolution resolution = new Aspose.Pdf.Devices.Resolution(150);

            // Initialize the BMP device. Width and Height set to 0 let the device use the page size.
            BmpDevice bmpDevice = new BmpDevice(0, 0, resolution);

            // Determine how many pages to process (pages 1‑20 or up to the document's page count)
            int pagesToConvert = Math.Min(20, pdfDocument.Pages.Count);

            // Loop through the selected pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pagesToConvert; pageNumber++)
            {
                // Construct the output BMP file name for each page
                string outputBmpPath = Path.Combine(dataDir, $"page_{pageNumber}.bmp");

                // Convert the current page to BMP and write it to a file stream
                using (FileStream bmpStream = new FileStream(outputBmpPath, FileMode.Create))
                {
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as BMP: {outputBmpPath}");
            }
        }

        Console.WriteLine("Conversion completed.");
    }
}
