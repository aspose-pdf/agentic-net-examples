using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add Bates numbering to each page.
            // Format: "2026-####" -> prefix "2026-" and 4 digits.
            doc.Pages.AddBatesNumbering(artifact =>
            {
                artifact.Prefix = "2026-";
                artifact.NumberOfDigits = 4;   // ensures four-digit numbering (####)
                artifact.StartNumber = 1;      // start from 1
                // Optional: set position or margins if needed.
            });

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates-numbered PDF saved to '{outputPath}'.");
    }
}