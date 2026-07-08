using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, BatesNArtifact, PageCollectionExtensions)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "bates_numbered.pdf"; // destination PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, add Bates numbering, and save.
        using (Document doc = new Document(inputPath))   // <-- load rule
        {
            // Configure and apply Bates numbering to every page.
            doc.Pages.AddBatesNumbering(artifact =>
            {
                artifact.Prefix        = "PRJ-"; // alphanumeric prefix for tracking
                artifact.StartNumber   = 1;      // start at 1
                artifact.NumberOfDigits = 6;    // zero‑padded to 6 digits (e.g., PRJ-000001)
            });

            doc.Save(outputPath);                     // <-- save rule
        }

        Console.WriteLine($"Bates numbering applied. Output saved to '{outputPath}'.");
    }
}