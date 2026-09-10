using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "PageImages";
        const string defaultFont = "Arial";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                // Prepare output image file path
                string imagePath = Path.Combine(outputFolder, $"Page_{pageNumber}.png");

                // Convert the current page to PNG using PngDevice
                using (FileStream imageStream = new FileStream(imagePath, FileMode.Create))
                {
                    // Configure rendering options for the device
                    RenderingOptions renderOpts = new RenderingOptions();
                    renderOpts.DefaultFontName = defaultFont;

                    // PngDevice renders a PDF page to a PNG image
                    PngDevice pngDevice = new PngDevice();
                    pngDevice.RenderingOptions = renderOpts;
                    pngDevice.Process(pdfDoc.Pages[pageNumber], imageStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as image: {imagePath}");
            }
        }

        Console.WriteLine("All pages have been converted to images.");
    }
}
