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
            // Define the resolution for the output BMP images (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Initialize the BMP device with the desired resolution
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // NOTE: In recent versions of Aspose.Pdf the CoordinateType enum has been removed.
            // The default behaviour of the image device already respects the page's CropBox,
            // so explicit assignment is unnecessary. If you are using an older version that
            // still provides the enum, you can uncomment the line below and reference the
            // appropriate enum (e.g., Aspose.Pdf.CoordinateType.CropBox).
            // bmpDevice.CoordinateType = Aspose.Pdf.CoordinateType.CropBox;

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build the output file path for each page
                string outputPath = Path.Combine(dataDir, $"image{pageNumber}_out.bmp");

                // Create a file stream for the BMP output
                using (FileStream bmpStream = new FileStream(outputPath, FileMode.Create))
                {
                    // Convert the current page to BMP and write it to the stream
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as BMP → {outputPath}");
            }
        }
    }
}
