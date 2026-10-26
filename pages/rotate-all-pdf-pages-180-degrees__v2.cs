using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Rotate each page 180 degrees using the correct property and enum value
                doc.Pages[i].Rotate = Rotation.on180;
            }

            // Save the rotated document (PDF format by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All pages rotated 180° and saved to '{outputPath}'.");
    }
}
