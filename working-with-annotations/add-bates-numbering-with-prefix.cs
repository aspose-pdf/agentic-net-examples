using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_bates.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add Bates numbering to each page with the desired configuration
            doc.Pages.AddBatesNumbering(artifact =>
            {
                artifact.Prefix          = "ABC";   // Prefix for the Bates number
                artifact.NumberOfDigits  = 6;       // Six‑digit format (default is 6)
                artifact.StartNumber     = 1;       // Starting number
                // Optional positioning – right‑aligned at the bottom with a margin
                artifact.ArtifactHorizontalAlignment = HorizontalAlignment.Right;
                artifact.ArtifactVerticalAlignment   = VerticalAlignment.Bottom;
                artifact.BottomMargin                = 20;
            });

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering added and saved to '{outputPath}'.");
    }
}