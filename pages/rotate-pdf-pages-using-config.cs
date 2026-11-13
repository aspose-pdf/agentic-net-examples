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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Configuration: page number (1‑based) -> rotation angle in degrees (must be 0, 90, 180, 270)
        var pageRotations = new Dictionary<int, int>
        {
            { 1, 90 },
            { 2, 180 },
            { 3, 270 },
            // add more entries as needed
        };

        // Map integer degrees to Aspose.Pdf.Rotation enum values
        var angleToRotation = new Dictionary<int, Rotation>
        {
            { 0,   Rotation.None },
            { 90,  Rotation.on90 },
            { 180, Rotation.on180 },
            { 270, Rotation.on270 }
        };

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                if (pageRotations.TryGetValue(i, out int angle) &&
                    angleToRotation.TryGetValue(angle, out Rotation rotationEnum))
                {
                    // Set the rotation for the current page using the Rotate property
                    doc.Pages[i].Rotate = rotationEnum;
                }
            }

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages rotated and saved to '{outputPath}'.");
    }
}
