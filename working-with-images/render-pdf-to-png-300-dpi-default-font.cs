using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;   // PngDevice resides here
using Aspose.Pdf.Text;      // SimpleFontSubstitution

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "output_images";

        // Verify source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Configure a font substitution so that any missing font is replaced with Arial.
        // This is the recommended way to set a default font in recent Aspose.Pdf versions.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Arial"));

        // Load PDF inside a using block (document-disposal-with-using rule)
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create a 300 DPI resolution object
            Resolution resolution = new Resolution(300);

            // Initialise the PNG device with the desired resolution
            PngDevice pngDevice = new PngDevice(resolution);

            // Iterate pages using 1‑based indexing (page-indexing-one-based rule)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];
                string outPath = Path.Combine(outputDir, $"page_{pageNum}.png");

                // Render the page to PNG and write to file
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    pngDevice.Process(page, outStream);
                }
            }
        }

        Console.WriteLine("All pages have been rendered to PNG at 300 DPI.");
    }
}
