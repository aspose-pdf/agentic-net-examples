using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string outputFolder = "output_png";        // folder for PNG files
        const double bradleyThreshold = 0.5;             // threshold for Bradley binarization (0.0‑1.0)

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Set desired resolution for PNG conversion (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution);

            // TiffDevice provides the Bradley binarization method
            TiffDevice tiffDevice = new TiffDevice();

            // Process each page
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Convert the page to a PNG image in memory
                using (MemoryStream pngStream = new MemoryStream())
                {
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                    pngStream.Position = 0; // reset for reading

                    // Apply Bradley binarization to the PNG image
                    using (MemoryStream binarizedStream = new MemoryStream())
                    {
                        tiffDevice.BinarizeBradley(pngStream, binarizedStream, bradleyThreshold);
                        binarizedStream.Position = 0; // reset for writing

                        // Save the binarized PNG to disk
                        string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");
                        File.WriteAllBytes(outputPath, binarizedStream.ToArray());
                        Console.WriteLine($"Page {pageNumber} saved as binarized PNG: {outputPath}");
                    }
                }
            }
        }
    }
}