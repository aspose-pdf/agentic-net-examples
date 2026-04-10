using System;
using System.IO;
using Aspose.Pdf; // Provides Document, Page, Rotation, etc.

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; iterate through all pages
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Apply a 90° rotation when the page number is divisible by 3
                if (i % 3 == 0)
                {
                    doc.Pages[i].Rotate = Rotation.on90; // corrected enum value
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}
