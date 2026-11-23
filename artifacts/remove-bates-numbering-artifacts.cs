using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_bates.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF, remove Bates numbering artifacts, and save
        using (Document doc = new Document(inputPath))
        {
            // Iterate through each page because artifacts are attached to pages, not the document.
            foreach (Page page in doc.Pages)
            {
                // ArtifactCollection uses 1‑based indexing and provides a Delete method.
                // Iterate backwards to safely remove items while enumerating.
                for (int i = page.Artifacts.Count; i >= 1; i--)
                {
                    Artifact artifact = page.Artifacts[i];
                    if (artifact is BatesNArtifact)
                    {
                        page.Artifacts.Delete(i);
                    }
                }
            }

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering removed. Saved to '{outputPath}'.");
    }
}
