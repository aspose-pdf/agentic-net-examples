using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text; // for FontRepository and SimpleFontSubstitution

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "page19.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify the document has at least 19 pages (Pages are 1‑based)
            if (doc.Pages.Count < 19)
            {
                Console.Error.WriteLine("The document contains fewer than 19 pages.");
                return;
            }

            // Register a fallback substitution so that any missing font is replaced with Courier New.
            // FontRepository.AddFont was removed in recent versions; use Substitutions instead.
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Courier New"));

            // Create a PNG device with 300 DPI resolution
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution);

            // Render the 19th page (Pages collection is 1‑based) to a PNG file
            using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
            {
                pngDevice.Process(doc.Pages[19], imageStream);
            }
        }

        Console.WriteLine($"Page 19 has been saved as PNG to '{outputPath}'.");
    }
}
