using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "page1.jpg";
        const int pageNumber = 1; // page to convert (1‑based index)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document pdfDoc = new Document(inputPath))
        {
            if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            // JpegDevice with default resolution (150 DPI) and maximum quality
            JpegDevice jpegDevice = new JpegDevice();

            using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
            {
                jpegDevice.Process(pdfDoc.Pages[pageNumber], imageStream);
            }
        }

        Console.WriteLine($"Page {pageNumber} saved as JPEG to '{outputPath}'.");
    }
}