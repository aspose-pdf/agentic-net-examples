using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

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

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Open the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Create a PNG device with 300 DPI resolution
            Aspose.Pdf.Devices.PngDevice pngDevice = new Aspose.Pdf.Devices.PngDevice(new Aspose.Pdf.Devices.Resolution(300));
            // Enable transparent background for UI thumbnails
            pngDevice.TransparentBackground = true;

            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Aspose.Pdf.Page page = pdfDoc.Pages[pageIndex];

                string outputPath = Path.Combine(outputFolder, $"page_{pageIndex}.png");
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    // Render the page to PNG with the specified DPI and transparency
                    pngDevice.Process(page, outStream);
                }

                Console.WriteLine($"Saved thumbnail: {outputPath}");
            }
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}