using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // BmpDevice for BMP conversion

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

        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        Document pdfDoc = new Document(pdfPath);

        // Define conversion parameters
        const int startPage = 1;
        int endPage = Math.Min(10, pdfDoc.Pages.Count); // limit to first 10 pages
        const int dpi = 150; // DPI – adjust as needed

        // BmpDevice expects a Resolution object, not a raw int
        Resolution resolution = new Resolution(dpi);
        BmpDevice bmpDevice = new BmpDevice(resolution);

        for (int pageNum = startPage; pageNum <= endPage; pageNum++)
        {
            string outPath = Path.Combine(outputDir, $"Page_{pageNum}.bmp");
            using (FileStream outStream = new FileStream(outPath, FileMode.Create))
            {
                // Convert the specific page to BMP and write to the file stream
                bmpDevice.Process(pdfDoc.Pages[pageNum], outStream);
            }
            Console.WriteLine($"Saved page {pageNum} as BMP to {outPath}");
        }
    }
}
