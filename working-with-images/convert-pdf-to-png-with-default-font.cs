using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "output_images";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Set the default font to use when a font is missing
            RenderingOptions renderOptions = new RenderingOptions();
            renderOptions.DefaultFontName = "Times New Roman";

            // Create a PNG device with a resolution of 300 DPI
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution);
            // Apply the rendering options to the device
            pngDevice.RenderingOptions = renderOptions;

            // Convert each page to an image
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create))
                {
                    pngDevice.Process(doc.Pages[pageNumber], outStream);
                }
                Console.WriteLine($"Page {pageNumber} saved as {outputPath}");
            }
        }
    }
}
