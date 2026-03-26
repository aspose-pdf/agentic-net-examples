using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // Required for PngDevice and Resolution

public class Program
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputFolder = "Images";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Convert each page to a separate PNG image file using PngDevice.
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                // Create a PNG device with the desired resolution (e.g., 300 DPI).
                var pngDevice = new PngDevice(new Resolution(300));

                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");

                // Process the specific page and write the image to the output file.
                pngDevice.Process(doc.Pages[pageNumber], outputPath);
            }
        }

        Console.WriteLine("All pages have been converted to images in folder: " + outputFolder);
    }
}
