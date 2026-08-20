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

        // Load the PDF, add Bates numbering, and save.
        using (Document doc = new Document(inputPath))
        {
            // Add Bates numbering to every page.
            // Start at 1000, use a dash as suffix, and display 4 digits.
            doc.Pages.AddBatesNumbering(artifact =>
            {
                artifact.StartNumber   = 1000;   // first Bates number
                artifact.NumberOfDigits = 4;     // ensures numbers like 1000, 1001, ...
                artifact.Suffix        = "-";    // dash separator after the number
                // Optional: set alignment or margins if needed
                // artifact.ArtifactHorizontalAlignment = HorizontalAlignment.Right;
            });

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates-numbered PDF saved to '{outputPath}'.");
    }
}