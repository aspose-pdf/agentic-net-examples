using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "output_images";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Font substitution: replace missing Helvetica with Times New Roman
        // The Substitutions collection works with SimpleFontSubstitution objects.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Times New Roman"));

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Define the resolution for the PNG images (150 DPI is a good default)
            var resolution = new Resolution(150);
            // PngDevice renders a page to a PNG stream without requiring System.Drawing
            var pngDevice = new PngDevice(resolution);

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                // Build the output PNG file path
                string outPath = Path.Combine(outputDir, $"page_{pageNumber}.png");

                // Render the current page to a PNG file
                using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    pngDevice.Process(doc.Pages[pageNumber], outStream);
                }
            }
        }

        Console.WriteLine("PDF successfully converted to PNG images.");
    }
}
