using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string outputFolder = "Thumbnails";         // folder for PNG images

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // High‑resolution (300 dpi) PNG device
            var pngDevice = new PngDevice(new Resolution(300));

            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                // Build output file name
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");

                // Render the page to PNG
                pngDevice.Process(pdfDoc.Pages[pageNumber], outputPath);
            }
        }

        Console.WriteLine($"Thumbnails created in folder: {outputFolder}");
    }
}
