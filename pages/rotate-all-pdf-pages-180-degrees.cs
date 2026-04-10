using System;
using System.IO;
using Aspose.Pdf;               // Core API namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages collection is 1‑based; iterate through all pages
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Set each page rotation to 180 degrees (on180)
                doc.Pages[i].Rotate = Rotation.on180;
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"All pages rotated 180° and saved to '{outputPath}'.");
    }
}