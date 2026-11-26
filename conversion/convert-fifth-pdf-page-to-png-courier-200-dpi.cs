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
        const string outputPath = "page5.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Substitute any missing font with Courier (acts as a default font for rendering)
        // SimpleFontSubstitution(sourceFont, targetFont)
        // Here we map a common font (e.g., Arial) to Courier; you can add more mappings as needed.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Arial", "Courier"));
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Courier"));
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Times New Roman", "Courier"));

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Verify the PDF has at least five pages (1‑based indexing)
            if (pdfDoc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The PDF contains fewer than 5 pages.");
                return;
            }

            // Define a 200 DPI resolution for the output image
            Resolution resolution = new Resolution(200);

            // Convert the 5th page to PNG using the specified resolution
            PngDevice pngDevice = new PngDevice(resolution);
            pngDevice.Process(pdfDoc.Pages[5], outputPath);
        }

        Console.WriteLine($"Page 5 has been saved as PNG to '{outputPath}'.");
    }
}
