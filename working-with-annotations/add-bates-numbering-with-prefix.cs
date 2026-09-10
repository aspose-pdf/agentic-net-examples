using System;
using System.IO;
using Aspose.Pdf; // Provides Document, PageCollectionExtensions, BatesNArtifact

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Add Bates numbering to each page using the extension method.
            // Configure the artifact with the required prefix and digit format.
            doc.Pages.AddBatesNumbering(bates =>
            {
                bates.Prefix = "ABC";          // Prefix to prepend to each number
                bates.NumberOfDigits = 6;      // Six‑digit format (default is 6, set explicitly)
                // Optional: bates.StartNumber = 1; // Starting number (default is 1)
            });

            // Save the modified document (lifecycle rule: Save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering added and saved to '{outputPath}'.");
    }
}