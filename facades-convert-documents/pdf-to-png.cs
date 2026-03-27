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
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution);

            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputFile = $"page{pageNumber}.png";
                using (FileStream pngStream = new FileStream(outputFile, FileMode.Create))
                {
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                }
                Console.WriteLine($"Saved {outputFile}");
            }
        }
    }
}