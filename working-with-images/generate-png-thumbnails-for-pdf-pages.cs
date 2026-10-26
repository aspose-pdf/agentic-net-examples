using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;   // PngDevice for rasterizing pages

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "Thumbnails";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Pages are 1‑based (see page-indexing-one-based rule)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                // Build the output file name for the current page
                string outputPath = Path.Combine(outputFolder, $"page_{pageIndex}.png");

                // PngDevice creates PNG images; the constructor (width, height) sets the thumbnail size
                PngDevice pngDevice = new PngDevice(150, 200);

                // Render the page to a PNG file
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    pngDevice.Process(pdfDoc.Pages[pageIndex], outStream);
                }
            }
        }

        Console.WriteLine("Thumbnail images generated successfully.");
    }
}