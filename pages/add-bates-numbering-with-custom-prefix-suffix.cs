using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bates_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Add Bates numbering to each page with custom prefix and suffix
                doc.Pages.AddBatesNumbering(artifact =>
                {
                    artifact.Prefix = "DOC";          // Custom prefix
                    artifact.Suffix = "-2026";        // Custom suffix
                    artifact.StartNumber = 1;         // Starting number
                    artifact.NumberOfDigits = 6;      // Optional: number of digits
                });

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Bates numbering applied. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}