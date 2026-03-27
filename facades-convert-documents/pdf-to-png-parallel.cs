using System;
using System.IO;
using System.Threading.Tasks;
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
            int pageCount = pdfDocument.Pages.Count;
            Parallel.For(1, pageCount + 1, pageNumber =>
            {
                var resolution = new Resolution(300);
                var pngDevice = new PngDevice(resolution);
                string outputFile = $"page{pageNumber}.png";

                // Directly write the page to a PNG file without creating a FileStream
                pngDevice.Process(pdfDocument.Pages[pageNumber], outputFile);
            });
        }

        Console.WriteLine("Conversion completed.");
    }
}
