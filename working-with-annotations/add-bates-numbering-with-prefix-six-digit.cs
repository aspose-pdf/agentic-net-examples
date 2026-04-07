using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bates.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add Bates numbering to each page with the desired settings
            doc.Pages.AddBatesNumbering(artifact =>
            {
                artifact.Prefix = "ABC";          // Prefix before the number
                artifact.NumberOfDigits = 6;      // Six‑digit format (e.g., 000001)
                // artifact.StartNumber = 1;      // Optional: starting number (default is 1)
            });

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering added. Saved to '{outputPath}'.");
    }
}