using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document pdfDocument = new Document(inputPath))
        {
            for (int pageNumber = 2; pageNumber <= 4; pageNumber++)
            {
                if (pageNumber > pdfDocument.Pages.Count)
                {
                    Console.WriteLine($"Page {pageNumber} does not exist. Skipping.");
                    continue;
                }

                string outputFile = $"page_{pageNumber}.png";

                using (FileStream imageStream = new FileStream(outputFile, FileMode.Create))
                {
                    Resolution resolution = new Resolution(200);
                    PngDevice pngDevice = new PngDevice(resolution);
                    pngDevice.TransparentBackground = true;
                    pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as {outputFile}");
            }
        }
    }
}