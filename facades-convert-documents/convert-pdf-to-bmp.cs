using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        using (Document pdfDocument = new Document(inputPdf))
        {
            Resolution resolution = new Resolution(150);
            BmpDevice bmpDevice = new BmpDevice(resolution);

            int maxPage = Math.Min(20, pdfDocument.Pages.Count);
            for (int pageNumber = 1; pageNumber <= maxPage; pageNumber++)
            {
                string outputFile = $"image{pageNumber}_out.bmp";
                using (FileStream bmpStream = new FileStream(outputFile, FileMode.Create))
                {
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }
                Console.WriteLine($"Saved page {pageNumber} as {outputFile}");
            }
        }
    }
}