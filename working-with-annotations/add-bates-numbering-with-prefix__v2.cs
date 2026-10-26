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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a BatesNArtifact for automatic numbering
            BatesNArtifact bates = new BatesNArtifact
            {
                Prefix = "ABC",          // Prefix for the Bates number
                StartNumber = 1,          // Starting number
                NumberOfDigits = 6,       // Six‑digit format (e.g., 000001)
                StartPage = 1,            // First page to apply numbering
                EndPage = doc.Pages.Count // Last page to apply numbering
            };

            // Add the artifact to each page's artifact collection (required because Document has no Artifacts collection)
            foreach (Page page in doc.Pages)
            {
                page.Artifacts.Add(bates);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering added and saved to '{outputPath}'.");
    }
}
