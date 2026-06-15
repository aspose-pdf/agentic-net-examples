using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "BmpImages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create a resolution of 150 DPI
            Resolution resolution = new Resolution(150);

            // Initialize BmpDevice with the desired resolution
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // Determine how many pages to process (up to 20)
            int pagesToConvert = Math.Min(20, pdfDocument.Pages.Count);

            // Loop through the selected pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pagesToConvert; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.bmp");

                // Convert the current page to BMP and save it
                using (FileStream bmpStream = new FileStream(outputPath, FileMode.Create))
                {
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }
            }
        }

        Console.WriteLine("PDF pages have been successfully converted to BMP images.");
    }
}