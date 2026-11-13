using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; iterate from the last page to the first
            for (int i = doc.Pages.Count; i >= 1; i--)
            {
                Page page = doc.Pages[i];
                // Rotate each page 90 degrees clockwise using the correct property
                page.Rotate = Rotation.on90;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}
