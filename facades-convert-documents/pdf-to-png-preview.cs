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
            Resolution resolution = new Resolution(72);
            PngDevice pngDevice = new PngDevice(resolution);

            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputImage = $"image{pageNumber}.png";
                using (FileStream imageStream = new FileStream(outputImage, FileMode.Create))
                {
                    pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                }
            }
        }

        Console.WriteLine("PNG preview images generated.");
    }
}