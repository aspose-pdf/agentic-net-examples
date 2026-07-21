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

        try
        {
            // Load the PDF document (lifecycle rule: wrap in using)
            using (Document doc = new Document(inputPath))
            {
                // Add Bates numbering to every page.
                // Prefix "2026-" and four digits (####) give IDs like 2026-0001, 2026-0002, ...
                doc.Pages.AddBatesNumbering(artifact =>
                {
                    artifact.Prefix        = "2026-";
                    artifact.NumberOfDigits = 4;   // ensures four-digit numbering
                    artifact.StartNumber    = 1;   // start from 1
                });

                // Save the modified PDF (lifecycle rule: Save inside using)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Bates numbering applied. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}