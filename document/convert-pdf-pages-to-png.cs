using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputImgDir = "SanitizedImages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputImgDir);

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Define the resolution for the output images (e.g., 300 DPI)
                var resolution = new Resolution(300);

                // Iterate through each page and export it as a PNG image
                for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
                {
                    var pngDevice = new PngDevice(resolution);
                    string outPath = Path.Combine(outputImgDir, $"page_{pageNumber}.png");
                    pngDevice.Process(pdfDoc.Pages[pageNumber], outPath);
                }
            }

            Console.WriteLine($"Sanitization complete. Images saved to '{outputImgDir}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}
