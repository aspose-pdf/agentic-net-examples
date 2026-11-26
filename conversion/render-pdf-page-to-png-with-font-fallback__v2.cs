using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text; // for SimpleFontSubstitution and FontRepository

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "page26.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Register a generic fallback to Times New Roman for any missing font.
        // The wildcard "*" applies the substitution to all fonts that cannot be found.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Times New Roman"));
        // Optionally add explicit substitution rules for specific missing fonts.
        // FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Times New Roman"));

        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 26 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 26)
            {
                Console.Error.WriteLine("The document contains fewer than 26 pages.");
                return;
            }

            // Retrieve the 26th page
            Page page = doc.Pages[26];

            // Define a 300 DPI resolution
            Resolution resolution = new Resolution(300);

            // PngDevice does not implement IDisposable, so do not use a using block.
            PngDevice pngDevice = new PngDevice(resolution);

            // Render the page to a PNG file
            using (FileStream outStream = new FileStream(outputPath, FileMode.Create))
            {
                pngDevice.Process(page, outStream);
            }
        }

        Console.WriteLine($"Page 26 rendered to PNG at 300 DPI: {outputPath}");
    }
}
