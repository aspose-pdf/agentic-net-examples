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
            // Add Bates numbering with an alphanumeric prefix
            doc.Pages.AddBatesNumbering(bates =>
            {
                bates.Prefix = "PRJ-";          // Alphanumeric prefix for project tracking
                bates.StartNumber = 1;          // Starting number
                bates.NumberOfDigits = 5;       // Zero‑padded to 5 digits (e.g., 00001)

                // Position the artifact at the bottom‑right of each page
                bates.ArtifactHorizontalAlignment = HorizontalAlignment.Right;
                bates.ArtifactVerticalAlignment   = VerticalAlignment.Bottom;
                bates.BottomMargin = 20;        // 20 points from the bottom edge
                bates.RightMargin  = 20;        // 20 points from the right edge
            });

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering added. Saved to '{outputPath}'.");
    }
}