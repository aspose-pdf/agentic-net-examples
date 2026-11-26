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
        const string outputPath = "page16.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Register a substitution so that any missing font is replaced with Helvetica
            // "*" means any font name that cannot be resolved.
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Helvetica"));

            // Verify the document has at least 16 pages (pages are 1‑based)
            if (doc.Pages.Count < 16)
            {
                Console.Error.WriteLine("The PDF contains fewer than 16 pages.");
                return;
            }

            // Retrieve the sixteenth page
            Page page = doc.Pages[16];

            // Define a resolution of 120 DPI
            Resolution resolution = new Resolution(120);

            // Create a PNG device with the specified resolution
            PngDevice pngDevice = new PngDevice(resolution);

            // Render the page to a PNG file
            pngDevice.Process(page, outputPath);
        }

        Console.WriteLine($"Page 16 has been rendered to PNG at '{outputPath}'.");
    }
}
