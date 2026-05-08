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
            // Add Bates numbering to each page
            doc.Pages.AddBatesNumbering(artifact =>
            {
                // Start numbering at 1000
                artifact.StartNumber = 1000;
                // Append a dash after each number (e.g., "1000-")
                artifact.Suffix = "-";
                // Optional: position the stamp at the bottom‑right corner
                artifact.ArtifactHorizontalAlignment = HorizontalAlignment.Right;
                artifact.BottomMargin = 10;
            });

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering applied. Output saved to '{outputPath}'.");
    }
}