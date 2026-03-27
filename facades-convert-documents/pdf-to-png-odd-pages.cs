using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

public class Program
{
    public static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution);

            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber += 2)
            {
                string outputFile = $"page{pageNumber}.png";
                pngDevice.Process(doc.Pages[pageNumber], outputFile);
                Console.WriteLine($"Saved page {pageNumber} as {outputFile}");
            }
        }
    }
}
