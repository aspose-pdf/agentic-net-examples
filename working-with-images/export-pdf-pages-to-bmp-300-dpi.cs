using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "BmpPages";

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
            // Create a BMP device with 300 DPI resolution (no using – BmpDevice is not IDisposable)
            var bmpDevice = new BmpDevice(new Resolution(300));

            // Aspose.Pdf uses 1‑based page indexing
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                // Build the output file name for the current page
                string outputPath = Path.Combine(outputFolder, $"Page_{pageNumber}.bmp");

                // Render the specific page to the BMP file via a FileStream
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    bmpDevice.Process(pdfDoc.Pages[pageNumber], outStream);
                }
            }
        }

        Console.WriteLine($"All pages exported to BMP at 300 DPI in folder: {outputFolder}");
    }
}
