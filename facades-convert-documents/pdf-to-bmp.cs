using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPattern = "page_{0}_out.bmp";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        using (Document pdfDocument = new Document(inputPdf))
        {
            Resolution resolution = new Resolution(300);
            BmpDevice bmpDevice = new BmpDevice(resolution);

            int startPage = 3;
            int endPage = 8;
            int maxPage = pdfDocument.Pages.Count;
            if (startPage > maxPage)
            {
                Console.Error.WriteLine("Start page exceeds document page count.");
                return;
            }

            int actualEnd = Math.Min(endPage, maxPage);
            for (int pageNumber = startPage; pageNumber <= actualEnd; pageNumber++)
            {
                string outputPath = string.Format(outputPattern, pageNumber);
                using (FileStream bmpStream = new FileStream(outputPath, FileMode.Create))
                {
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }
                Console.WriteLine($"Saved page {pageNumber} as {outputPath}");
            }
        }
    }
}