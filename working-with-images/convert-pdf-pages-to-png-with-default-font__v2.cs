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

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build the output file name for the current page
                string outputImagePath = Path.Combine(outputFolder, $"Page_{pageNumber}.png");

                // Create a PNG device to render the page as an image
                PngDevice pngDevice = new PngDevice();

                // Set rendering options, including the default font substitution
                pngDevice.RenderingOptions = new RenderingOptions
                {
                    DefaultFontName = "Times New Roman"
                };

                // Render the page to the specified PNG file
                pngDevice.Process(pdfDocument.Pages[pageNumber], outputImagePath);
            }
        }

        Console.WriteLine("PDF pages have been converted to images successfully.");
    }
}