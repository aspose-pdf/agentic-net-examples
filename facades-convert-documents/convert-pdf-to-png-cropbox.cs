using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        // Load the PDF document.
        Document pdfDoc = new Document(inputPdf);

        int pageNumber = 1;
        foreach (Page page in pdfDoc.Pages)
        {
            // Preserve the original MediaBox.
            var originalMediaBox = page.MediaBox;

            // If a CropBox is defined, use it as the rendering area.
            // Aspose.Pdf devices render the MediaBox, so we temporarily replace it with the CropBox.
            if (page.CropBox != null && page.CropBox.Width > 0 && page.CropBox.Height > 0)
            {
                page.MediaBox = page.CropBox;
            }

            string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
            using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
            {
                // You can adjust the resolution as needed.
                var resolution = new Resolution(300);
                var pngDevice = new PngDevice(resolution);
                pngDevice.Process(page, imageStream);
            }

            // Restore the original MediaBox to avoid side‑effects on subsequent operations.
            page.MediaBox = originalMediaBox;
            pageNumber++;
        }

        Console.WriteLine("PDF successfully converted to PNG images.");
    }
}
