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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add Bates numbering to all pages
            // StartNumber = 1000, Suffix = "-" creates numbers like "1000-", "1001-", etc.
            doc.Pages.AddBatesNumbering(artifact =>
            {
                artifact.StartNumber = 1000;
                artifact.Suffix = "-";
                // Optional: configure number of digits, alignment, etc.
                // artifact.NumberOfDigits = 6;
            });

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering added. Saved to '{outputPath}'.");
    }
}