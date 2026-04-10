using System;
using System.IO;
using Aspose.Pdf; // Document, BatesNArtifact, PageCollectionExtensions

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

        // Load the PDF, add Bates numbering, and save.
        using (Document doc = new Document(inputPath))
        {
            // Add Bates numbering to every page.
            doc.Pages.AddBatesNumbering(bates =>
            {
                bates.Prefix        = "DOC";    // Custom prefix
                bates.Suffix        = "-2026";  // Custom suffix
                bates.StartNumber   = 1;        // Starting number
                bates.NumberOfDigits = 6;       // Optional: total digits (default 6)
            });

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates-numbered PDF saved to '{outputPath}'.");
    }
}