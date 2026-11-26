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
        const string outputPath = "page7.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Register Verdana as a fallback for any missing glyphs.
            string verdanaPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "verdana.ttf");
            if (File.Exists(verdanaPath))
            {
                // In recent Aspose.PDF versions the AddFont method was removed.
                // Register the font via a substitution rule instead.
                FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Verdana"));
            }
            else
            {
                Console.Error.WriteLine("Verdana font not found – substitution will use the default font.");
            }

            // Ensure the document has at least 7 pages (1‑based indexing).
            if (doc.Pages.Count < 7)
            {
                Console.Error.WriteLine("The document contains fewer than 7 pages.");
                return;
            }

            // Retrieve the seventh page.
            Page page = doc.Pages[7];

            // Create a PNG device with a resolution of 300 DPI.
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution);

            // Render the page directly to a PNG file.
            pngDevice.Process(page, outputPath);
        }

        Console.WriteLine($"Seventh page saved as PNG to '{outputPath}'.");
    }
}
