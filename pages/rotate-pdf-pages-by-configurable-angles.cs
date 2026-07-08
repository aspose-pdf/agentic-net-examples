using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        // Configuration: page number (1‑based) -> rotation angle in degrees (0, 90, 180, 270)
        var rotationConfig = new Dictionary<int, int>
        {
            { 1, 90 },   // rotate page 1 by 90°
            { 3, 180 },  // rotate page 3 by 180°
            { 5, 270 }   // rotate page 5 by 270°
        };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (page indexing is 1‑based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                if (rotationConfig.TryGetValue(i, out int angle))
                {
                    // Convert integer angle to the Rotation enum (static helper on Page)
                    Rotation rotationEnum = Page.IntToRotation(angle);
                    doc.Pages[i].Rotate = rotationEnum;
                }
            }

            // Save the modified document (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}