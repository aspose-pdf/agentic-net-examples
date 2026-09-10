using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF path – replace with your actual file
        const string inputPdfPath = "input.pdf";
        // Output image path – PNG format used for demonstration
        const string outputImagePath = "output.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a PNG device to render PDF pages as images
            PngDevice pngDevice = new PngDevice();

            // Configure rendering options to improve anti‑aliasing quality
            pngDevice.RenderingOptions = new RenderingOptions
            {
                // Enables high‑quality interpolation for smoother curves and lines
                InterpolationHighQuality = true,
                // Optional: enable font hinting for better text rendering
                UseFontHinting = true
            };

            // Render the first page (Aspose.Pdf uses 1‑based indexing)
            pngDevice.Process(pdfDoc.Pages[1], outputImagePath);
        }

        Console.WriteLine($"Page rendered with enhanced anti‑aliasing to '{outputImagePath}'.");
    }
}