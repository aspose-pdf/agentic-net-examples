using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Directory where each page image will be saved
        const string outputImageDir = "SanitizedImages";

        // Validate input file existence
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputImageDir);

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Define the resolution for the rasterized images (e.g., 300 DPI)
                var resolution = new Resolution(300);
                // Create an image device – PNG in this example
                var pngDevice = new PngDevice(resolution);

                // Iterate through all pages and save each as a separate image file
                for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
                {
                    string outPath = Path.Combine(outputImageDir, $"page_{pageNumber}.png");
                    pngDevice.Process(pdfDoc.Pages[pageNumber], outPath);
                }
            }

            Console.WriteLine($"All pages have been saved as images in '{outputImageDir}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
