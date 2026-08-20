using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // BmpDevice resides here

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExportedBmpImages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (using statement ensures proper disposal)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create a Resolution object for 300 DPI
            Resolution resolution = new Resolution(300);

            // Initialize BmpDevice with the custom resolution
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // OPTIONAL: customize rendering options for higher quality
            // bmpDevice.RenderingOptions = new RenderingOptions
            // {
            //     // Example settings (adjust as needed)
            //     // AntiAliasing = true,
            //     // TextRenderingMode = TextRenderingMode.AntiAliased
            // };

            // Iterate through all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.bmp");

                // Create a file stream for the BMP output
                using (FileStream bmpStream = new FileStream(outputPath, FileMode.Create))
                {
                    // Convert the current page to BMP and write to the stream
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }
            }
        }

        Console.WriteLine("PDF pages have been exported as BMP images at 300 DPI.");
    }
}