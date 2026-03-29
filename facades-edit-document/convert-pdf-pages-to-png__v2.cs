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
            // Create a PNG device with 200 DPI and transparent background
            Aspose.Pdf.Devices.PngDevice pngDevice = new Aspose.Pdf.Devices.PngDevice(new Aspose.Pdf.Devices.Resolution(200));
            pngDevice.TransparentBackground = true;

            int startPage = 2;
            int endPage = 4;
            int maxPage = pdfDocument.Pages.Count;
            if (endPage > maxPage)
            {
                endPage = maxPage;
            }

            for (int pageNumber = startPage; pageNumber <= endPage; pageNumber++)
            {
                Aspose.Pdf.Page page = pdfDocument.Pages[pageNumber];
                string outputFileName = $"page{pageNumber}.png";

                using (FileStream imageStream = new FileStream(outputFileName, FileMode.Create, FileAccess.Write))
                {
                    pngDevice.Process(page, imageStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as {outputFileName}");
            }
        }
    }
}