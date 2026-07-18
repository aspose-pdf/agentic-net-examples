using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class RotatePagesExample
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Configuration: page number (1‑based) -> desired rotation.
        // Rotation enum values use the 'on' prefix (on90, on180, on270, on360).
        var pageRotations = new Dictionary<int, Rotation>
        {
            { 1, Rotation.on90 },   // rotate page 1 by 90°
            { 2, Rotation.on180 },  // rotate page 2 by 180°
            // add more entries as needed
        };

        // Load the PDF, modify page rotations, and save.
        using (Document doc = new Document(inputPath))
        {
            // Pages collection is 1‑based.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                if (pageRotations.TryGetValue(i, out Rotation rot))
                {
                    doc.Pages[i].Rotate = rot;
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}
