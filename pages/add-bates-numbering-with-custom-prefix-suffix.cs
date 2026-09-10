using System;
using System.IO;
using Aspose.Pdf; // Document, PageCollection, BatesNArtifact, extension methods

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

        // Load the PDF, add Bates numbering, and save.
        using (Document doc = new Document(inputPath))
        {
            // Add Bates numbering to every page.
            // Configure the artifact with the required prefix and suffix.
            doc.Pages.AddBatesNumbering(artifact =>
            {
                artifact.Prefix = "DOC";      // Custom prefix
                artifact.Suffix = "-2026";    // Custom suffix
                artifact.StartNumber = 1;     // Optional: start at 1
                artifact.NumberOfDigits = 6;  // Optional: six‑digit numbering (default)
            });

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates‑numbered PDF saved to '{outputPath}'.");
    }
}