using System;
using System.IO;
using Aspose.Pdf;

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
            // Prefix "2026-" and four digits (####) produce IDs like 2026-0001, 2026-0002, etc.
            doc.Pages.AddBatesNumbering(artifact =>
            {
                artifact.Prefix        = "2026-";
                artifact.NumberOfDigits = 4;   // ####
                artifact.StartNumber    = 1;   // start from 1
                // Additional settings (e.g., alignment, margins) can be configured here if needed.
            });

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering applied. Output saved to '{outputPath}'.");
    }
}