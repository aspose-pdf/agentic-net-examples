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

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document; the using block guarantees proper disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // BmpDevice with default resolution
            BmpDevice bmpDevice = new BmpDevice();

            // Pages are 1‑based indexed in Aspose.Pdf
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string bmpPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");

                // Write each page to a BMP file using a FileStream inside a using block
                using (FileStream bmpStream = new FileStream(bmpPath, FileMode.Create))
                {
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as BMP to '{bmpPath}'.");
            }
        }
    }
}