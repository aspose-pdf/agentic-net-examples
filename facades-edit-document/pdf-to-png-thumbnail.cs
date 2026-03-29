using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPattern = "page_{0}.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        using (Document pdfDocument = new Document(inputPdf))
        {
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution);
            pngDevice.TransparentBackground = true;

            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputFile = string.Format(outputPattern, pageNumber);
                using (FileStream pngStream = new FileStream(outputFile, FileMode.Create))
                {
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                }
            }
        }

        Console.WriteLine("Conversion completed.");
    }
}
