using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "output";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        using (Document pdfDocument = new Document(inputPath))
        {
            // Use default resolution GifDevice
            GifDevice gifDevice = new GifDevice();

            for (int pageNum = 1; pageNum <= pdfDocument.Pages.Count; pageNum++)
            {
                string outPath = Path.Combine(outputDir, $"page{pageNum}.gif");
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    gifDevice.Process(pdfDocument.Pages[pageNum], outStream);
                }
            }
        }

        Console.WriteLine("Conversion completed.");
    }
}
