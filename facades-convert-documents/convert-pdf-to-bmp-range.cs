using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

public class Program
{
    public static void Main()
    {
        string pdfPath = "input.pdf";
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        using (Document pdfDocument = new Document(pdfPath))
        {
            Resolution resolution = new Resolution(300);
            BmpDevice bmpDevice = new BmpDevice(resolution);

            int startPage = 2;
            int endPage = 6;
            int maxPage = pdfDocument.Pages.Count;
            if (startPage > maxPage)
            {
                Console.Error.WriteLine("Start page exceeds document page count.");
                return;
            }

            int actualEnd = Math.Min(endPage, maxPage);
            for (int pageNumber = startPage; pageNumber <= actualEnd; pageNumber++)
            {
                string bmpFileName = $"image{pageNumber}_out.bmp";
                using (FileStream bmpStream = new FileStream(bmpFileName, FileMode.Create))
                {
                    bmpDevice.Process(pdfDocument.Pages[pageNumber], bmpStream);
                }
                Console.WriteLine($"Saved page {pageNumber} as {bmpFileName}");
            }
        }
    }
}