using System;
using System.IO;
using Aspose.Pdf; // Page, Document, Rotation enum

class Program
{
    static void Main()
    {
        const string inputPath  = "rotated_input.pdf";
        const string outputPath = "restored_output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Reset page rotation to its original (non‑rotated) state
                // Rotation.None corresponds to 0° orientation
                doc.Pages[i].Rotate = Rotation.None;
            }

            // Save the modified document (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with original orientation: '{outputPath}'.");
    }
}