using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class RotatePagesExample
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        // Configuration: page number (1‑based) -> rotation angle in degrees (0, 90, 180, 270)
        var rotationConfig = new Dictionary<int, int>
        {
            { 1, 90 },   // rotate page 1 by 90°
            { 2, 180 },  // rotate page 2 by 180°
            { 5, 270 }   // rotate page 5 by 270°
            // add more entries as needed
        };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, apply rotations, and save
        using (Document doc = new Document(inputPath))
        {
            // Pages collection is 1‑based
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                if (rotationConfig.TryGetValue(i, out int angle))
                {
                    // Convert integer angle to the Rotation enum
                    doc.Pages[i].Rotate = Page.IntToRotation(angle);
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}