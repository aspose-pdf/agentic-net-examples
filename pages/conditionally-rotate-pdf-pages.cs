using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF, rotate pages conditionally, and save.
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Apply a 90° rotation when the page number is divisible by 3.
                if (i % 3 == 0)
                {
                    // Correct enum value uses the "on" prefix.
                    doc.Pages[i].Rotate = Rotation.on90;
                }
            }

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}
