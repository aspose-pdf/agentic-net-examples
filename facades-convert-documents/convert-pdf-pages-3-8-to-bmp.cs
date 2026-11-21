using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "BmpPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document.
            Document pdfDocument = new Document(inputPdf);

            // Define the page range (inclusive).
            int startPage = 3;
            int endPage   = 8;
            // Clamp the range to the actual number of pages.
            if (startPage < 1) startPage = 1;
            if (endPage > pdfDocument.Pages.Count) endPage = pdfDocument.Pages.Count;

            // Set the desired resolution (dots per inch).
            Resolution resolution = new Resolution(150);

            // BmpDevice is a concrete device but does NOT implement IDisposable.
            // Therefore we instantiate it once and reuse it without a using block.
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // Iterate through the selected pages and render each as a BMP image.
            for (int pageNumber = startPage; pageNumber <= endPage; pageNumber++)
            {
                string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.bmp");
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], outStream);
                }
                Console.WriteLine($"Saved page {pageNumber} as BMP → {outPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
