using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "cleaned_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Replace transparent content with opaque raster/vector graphics.
            // This removes hidden elements that rely on transparency.
            doc.FlattenTransparency();

            // Remove any unused resources (fonts, images, etc.) that may be hidden.
            doc.OptimizeResources();

            // Optionally remove PDF/A and PDF/UA compliance if present.
            // This can strip hidden accessibility structures that are not needed.
            doc.RemovePdfaCompliance();
            doc.RemovePdfUaCompliance();

            // Save the cleaned PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hidden content eliminated. Output saved to '{outputPath}'.");
    }
}