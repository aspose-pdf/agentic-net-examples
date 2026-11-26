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
        const string outputPath = "page1.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Register Arial as a substitution for any missing font.
        // This makes Arial the default font used during rendering.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Arial", "Arial"));

        // Load the PDF document.
        using (Document doc = new Document(inputPath))
        {
            // Create a PNG device with a resolution of 300 DPI.
            PngDevice pngDevice = new PngDevice(new Resolution(300));

            // Render the first page (1‑based index) to a PNG file.
            using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
            {
                pngDevice.Process(doc.Pages[1], imageStream);
            }
        }

        Console.WriteLine($"First page saved as PNG to '{outputPath}'.");
    }
}
