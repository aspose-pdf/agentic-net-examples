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

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Set desired image resolution (dpi)
            var resolution = new Resolution(300);

            // Create a PNG device that will render each page
            var pngDevice = new PngDevice(resolution);

            // Iterate through all pages (1‑based indexing as required by Aspose.Pdf)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                // Render the current page to a memory stream
                using (var imgStream = new MemoryStream())
                {
                    pngDevice.Process(pdfDoc.Pages[pageNumber], imgStream);

                    // Build output file name with sequential numbering
                    string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");

                    // Write the PNG bytes to disk
                    File.WriteAllBytes(outputPath, imgStream.ToArray());
                }
            }
        }

        Console.WriteLine("PDF has been converted to PNG images successfully.");
    }
}
