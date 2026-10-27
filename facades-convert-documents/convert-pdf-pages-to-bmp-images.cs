using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "BmpPages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Define the page range (2 through 6 inclusive). Guard against PDFs with fewer pages.
            int startPage = 2;
            int endPage   = Math.Min(6, pdfDoc.Pages.Count);

            // Set the desired resolution (dots per inch)
            var resolution = new Resolution(150);

            // BmpDevice does NOT implement IDisposable, so instantiate it without a using block
            var bmpDevice = new BmpDevice(resolution);

            // Loop through the selected pages and render each one as a BMP image
            for (int pageNumber = startPage; pageNumber <= endPage; pageNumber++)
            {
                string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.bmp");

                // Dispose only the file stream; the device does not need disposal
                using (var outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    bmpDevice.Process(pdfDoc.Pages[pageNumber], outStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as BMP to '{outPath}'.");
            }
        }
    }
}
