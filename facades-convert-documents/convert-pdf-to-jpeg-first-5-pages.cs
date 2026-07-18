using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "output_images";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load PDF document inside a using block for proper disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create a Resolution object with 200 DPI
            Resolution resolution = new Resolution(200);

            // Initialize JpegDevice with the specified resolution
            JpegDevice jpegDevice = new JpegDevice(resolution);

            // Determine how many pages to process (max 5)
            int pagesToConvert = Math.Min(5, pdfDocument.Pages.Count);

            // Iterate over pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pagesToConvert; pageIndex++)
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageIndex}.jpeg");

                // Convert the page to JPEG and write to file
                using (FileStream jpegStream = new FileStream(outputPath, FileMode.Create))
                {
                    jpegDevice.Process(pdfDocument.Pages[pageIndex], jpegStream);
                }

                Console.WriteLine($"Page {pageIndex} saved as JPEG to '{outputPath}'.");
            }
        }
    }
}