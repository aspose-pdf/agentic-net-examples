using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // <-- PngDevice lives here in recent versions
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "PngOutput";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Set up font substitution: replace missing Helvetica with Times New Roman
        SimpleFontSubstitution substitution = new SimpleFontSubstitution("Helvetica", "Times New Roman", false);
        FontRepository.Substitutions.Add(substitution);

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a PNG device for rendering pages
            PngDevice pngDevice = new PngDevice(); // default resolution (96 DPI). Adjust if needed.

            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                string outPath = Path.Combine(outputDir, $"page_{i}.png");
                // Render the current page to a PNG file
                pngDevice.Process(doc.Pages[i], outPath);
                Console.WriteLine($"Saved {outPath}");
            }
        }
    }
}
