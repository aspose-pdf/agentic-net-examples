using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Create rendering options with the desired default font.
        var renderingOptions = new RenderingOptions
        {
            DefaultFontName = "Arial"
        };

        // Load the PDF document.
        using (Document doc = new Document(inputPdf))
        {
            // Create a PNG device and attach the rendering options.
            var pngDevice = new PngDevice();
            pngDevice.RenderingOptions = renderingOptions;

            // Convert each page to a PNG image.
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                string imagePath = Path.Combine(outputDir, $"page_{pageNumber}.png");
                using (FileStream imageStream = new FileStream(imagePath, FileMode.Create))
                {
                    pngDevice.Process(doc.Pages[pageNumber], imageStream);
                }
            }
        }

        Console.WriteLine($"Pages converted to images in '{outputDir}'.");
    }
}
