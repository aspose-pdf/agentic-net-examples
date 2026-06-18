using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API (Document, Page, BatesNArtifact, etc.)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "bates_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add Bates numbering to every page.
            // StartNumber = 1000, Suffix = "-" adds a dash after each number (e.g., "1000-").
            doc.Pages.AddBatesNumbering(artifact =>
            {
                artifact.StartNumber = 1000;   // Starting Bates number
                artifact.Suffix      = "-";   // Dash separator after the number
                // Optional: set number of digits if a fixed width is required
                // artifact.NumberOfDigits = 6;
            });

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering applied and saved to '{outputPath}'.");
    }
}