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
        const string outputPath = "page3.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Substitute any missing font with Helvetica. This is the recommended way
            // to provide a default font when the original font cannot be found.
            // Adjust the source font name as needed; using a wildcard substitution is not supported,
            // so we add a generic substitution that covers common missing fonts.
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("Arial", "Helvetica"));
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("Times New Roman", "Helvetica"));
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("Courier New", "Helvetica"));

            // Verify the document has at least three pages (pages are 1‑based)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The PDF contains fewer than 3 pages.");
                return;
            }

            // Retrieve the third page (1‑based indexing)
            Page page = doc.Pages[3];

            // Create a PNG device with a resolution of 150 DPI
            Resolution resolution = new Resolution(150);
            PngDevice pngDevice = new PngDevice(resolution);

            // Convert the page to PNG and save to the specified path
            pngDevice.Process(page, outputPath);
        }

        Console.WriteLine($"Third page saved as PNG to '{outputPath}'.");
    }
}
