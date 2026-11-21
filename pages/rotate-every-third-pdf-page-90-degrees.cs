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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing (see page-indexing-one-based rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Rotate 90 degrees clockwise when the page number is divisible by 3
                if (i % 3 == 0)
                {
                    // Use the Rotation enum instead of an integer literal
                    doc.Pages[i].Rotate = Rotation.on90;
                }
            }

            // Save the modified document (PDF format by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}
