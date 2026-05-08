using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class PdfToImages
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "Images";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Define the resolution for the output images (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);
            // Create a PNG device that will render pages to PNG images
            PngDevice pngDevice = new PngDevice(resolution);

            // Configure rendering options – set the default font to substitute missing fonts
            pngDevice.RenderingOptions = new RenderingOptions();
            pngDevice.RenderingOptions.DefaultFontName = "Times New Roman";

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"Page_{pageNumber}.png");
                // Render the current page to an image file
                pngDevice.Process(pdfDoc.Pages[pageNumber], outputPath);
            }
        }

        Console.WriteLine($"All pages have been saved to '{outputFolder}'.");
    }
}
