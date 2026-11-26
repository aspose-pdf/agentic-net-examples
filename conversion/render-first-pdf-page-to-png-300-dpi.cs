using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "page1.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            if (doc.Pages.Count < 1)
            {
                Console.Error.WriteLine("PDF contains no pages.");
                return;
            }

            // Create PNG device with 300 DPI resolution
            var pngDevice = new PngDevice(new Resolution(300))
            {
                TransparentBackground = true // optional, makes background transparent
            };

            // Render the first page to PNG file
            pngDevice.Process(doc.Pages[1], outputPath);
        }

        Console.WriteLine($"First page rendered to PNG at 300 DPI: {outputPath}");
    }
}
