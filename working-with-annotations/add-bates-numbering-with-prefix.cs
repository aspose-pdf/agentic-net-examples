using System;
using System.IO;
using Aspose.Pdf; // Document, PageCollectionExtensions, BatesNArtifact

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "bates_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Add Bates numbering to every page.
            // The Action configures the BatesNArtifact for each page.
            doc.Pages.AddBatesNumbering(artifact =>
            {
                artifact.Prefix = "ABC";          // Prefix before the number
                artifact.NumberOfDigits = 6;      // Six‑digit format (default is 6)
                artifact.StartNumber = 1;         // Optional: start from 1
                // Additional optional settings (e.g., alignment) can be set here if needed
            });

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering added and saved to '{outputPath}'.");
    }
}