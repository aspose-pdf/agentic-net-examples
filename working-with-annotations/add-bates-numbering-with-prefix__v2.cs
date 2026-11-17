using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_bates.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Add Bates numbering artifact to each page (Artifacts belong to Page, not Document)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                var page = doc.Pages[i];
                var bates = new BatesNArtifact
                {
                    Prefix = "ABC",          // Desired prefix
                    NumberOfDigits = 6,       // Six‑digit format (e.g., ABC000001)
                    StartNumber = 1,          // Starting number
                    // Increment property does not exist in this version of Aspose.PDF; default increment is 1
                    StartPage = 1,            // Apply from the first page
                    EndPage = doc.Pages.Count // Apply to the last page
                };
                page.Artifacts.Add(bates);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering added and saved to '{outputPath}'.");
    }
}
