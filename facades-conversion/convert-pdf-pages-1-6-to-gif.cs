using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "GifPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        var pdfDocument = new Document(inputPdf);

        // Define the page range to convert (pages 1 through 6)
        int startPage = 1;
        int endPage = Math.Min(6, pdfDocument.Pages.Count);

        // Set the desired resolution (dots per inch)
        var resolution = new Resolution(150);

        // Convert each page in the range to a GIF image using GifDevice
        for (int pageNumber = startPage; pageNumber <= endPage; pageNumber++)
        {
            string outPath = Path.Combine(outputFolder, $"page_{pageNumber}.gif");
            using (var outStream = new FileStream(outPath, FileMode.Create))
            {
                var gifDevice = new GifDevice(resolution);
                gifDevice.Process(pdfDocument.Pages[pageNumber], outStream);
            }
        }

        Console.WriteLine($"Pages {startPage}‑{endPage} have been saved as GIF images in '{outputFolder}'.");
    }
}
