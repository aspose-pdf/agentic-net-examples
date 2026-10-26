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
        const string outputPath = "output.tiff";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Apply a default font substitution for rendering (e.g., map any missing font to Arial)
            // This ensures that text is rendered even if the original font is unavailable.
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Arial"));

            // Define the resolution for the TIFF output (300 DPI)
            Resolution resolution = new Resolution(300);

            // Create a TiffDevice. The overload without explicit compression uses CCITT Group4 by default.
            TiffDevice tiffDevice = new TiffDevice(resolution);

            // Convert the entire PDF to a multi‑page TIFF
            using (FileStream outStream = new FileStream(outputPath, FileMode.Create))
            {
                tiffDevice.Process(doc, outStream);
            }
        }

        Console.WriteLine($"PDF successfully converted to TIFF: {outputPath}");
    }
}
