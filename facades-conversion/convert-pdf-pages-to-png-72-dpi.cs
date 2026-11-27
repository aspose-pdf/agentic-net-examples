using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Define the resolution (72 DPI) – this reduces the image size
        Resolution resolution = new Resolution(72);

        // Convert the first two pages (or fewer if the document has less pages) to PNG
        int pagesToConvert = Math.Min(2, pdfDocument.Pages.Count);
        for (int pageNumber = 1; pageNumber <= pagesToConvert; pageNumber++)
        {
            string outPath = Path.Combine(outputDir, $"page_{pageNumber}.png");

            using (FileStream imageStream = new FileStream(outPath, FileMode.Create))
            {
                // PngDevice renders a PDF page to a PNG image using the specified resolution
                PngDevice pngDevice = new PngDevice(resolution);
                pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
            }

            Console.WriteLine($"Page {pageNumber} saved as {outPath}");
        }
    }
}
