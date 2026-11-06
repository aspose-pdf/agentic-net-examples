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

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Define page range (first 10 pages or fewer)
        int startPage = 1;
        int endPage = Math.Min(10, pdfDocument.Pages.Count);

        // Set desired resolution (dots per inch)
        Resolution resolution = new Resolution(150);

        // Create a BMP device for rendering pages (devices are NOT IDisposable)
        BmpDevice bmpDevice = new BmpDevice(resolution);

        for (int pageNumber = startPage; pageNumber <= endPage; pageNumber++)
        {
            string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.bmp");
            using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
            {
                bmpDevice.Process(pdfDocument.Pages[pageNumber], outStream);
            }
            Console.WriteLine($"Saved {outPath}");
        }
    }
}
