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

        // Load the PDF, add Bates numbering with a prefix, then save.
        using (Document doc = new Document(inputPath))
        {
            // Configure and add Bates numbering to every page.
            doc.Pages.AddBatesNumbering(artifact =>
            {
                artifact.Prefix          = "PRJ-";               // Alphanumeric prefix
                artifact.StartNumber     = 1;                    // Starting number
                artifact.NumberOfDigits = 5;                    // Zero‑pad to 5 digits (e.g., PRJ-00001)
                artifact.ArtifactHorizontalAlignment = HorizontalAlignment.Right;
                artifact.ArtifactVerticalAlignment   = VerticalAlignment.Bottom;
                // Optional: adjust margins if needed
                artifact.RightMargin = 20;
                artifact.BottomMargin = 20;
            });

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering added. Output saved to '{outputPath}'.");
    }
}