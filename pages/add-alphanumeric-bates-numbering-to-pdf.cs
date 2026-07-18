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

        // Load the PDF, add Bates numbering with a prefix, then save.
        using (Document doc = new Document(inputPath))
        {
            // Configure the Bates numbering artifact via the Action overload.
            doc.Pages.AddBatesNumbering(artifact =>
            {
                artifact.Prefix = "PRJ-";          // Alphanumeric prefix for project tracking
                artifact.StartNumber = 1;          // Starting number
                artifact.NumberOfDigits = 5;       // Zero‑padded to 5 digits (e.g., PRJ-00001)
                // Optional: customize position, alignment, etc., if needed.
            });

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering applied and saved to '{outputPath}'.");
    }
}