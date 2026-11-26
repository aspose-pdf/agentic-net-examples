using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "page4.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Register Times New Roman as a fallback for any missing font
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Times New Roman"));

            // Ensure the document has at least four pages (1‑based indexing)
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The document contains fewer than 4 pages.");
                return;
            }

            // Create a PNG device with 300 DPI resolution
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution);

            // Render the fourth page to a PNG file
            pngDevice.Process(doc.Pages[4], outputPath);
        }

        Console.WriteLine($"Fourth page rendered to PNG: {outputPath}");
    }
}
