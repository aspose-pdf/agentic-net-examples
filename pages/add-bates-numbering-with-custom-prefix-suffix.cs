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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Add Bates numbering to every page.
            // The action configures the BatesNArtifact for each page.
            doc.Pages.AddBatesNumbering(artifact =>
            {
                artifact.Prefix       = "DOC";      // Custom prefix.
                artifact.Suffix       = "-2026";    // Custom suffix.
                artifact.StartNumber  = 1;          // Starting number (optional, default is 1).
                // artifact.NumberOfDigits = 6;     // Optional: set digit width if needed.
            });

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates-numbered PDF saved to '{outputPath}'.");
    }
}