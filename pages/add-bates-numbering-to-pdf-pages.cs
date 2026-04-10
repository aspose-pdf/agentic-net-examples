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

        // Load the PDF document (using the standard Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Add Bates numbering to every page.
            // Configure the artifact: prefix "2026-", 4 digits, start at 1.
            doc.Pages.AddBatesNumbering(artifact =>
            {
                artifact.Prefix = "2026-";
                artifact.NumberOfDigits = 4;   // ####
                artifact.StartNumber = 1;
            });

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates-numbered PDF saved to '{outputPath}'.");
    }
}