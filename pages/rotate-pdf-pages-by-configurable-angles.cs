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

        // Configuration: page number (1‑based) -> rotation angle in degrees
        // Only multiples of 90 are supported by the Rotation enum.
        var pageRotations = new Dictionary<int, int>
        {
            { 1, 90 },   // Rotate page 1 by 90°
            { 2, 180 },  // Rotate page 2 by 180°
            { 3, 270 }   // Rotate page 3 by 270°
            // Add more entries as needed
        };

        // Load the PDF, apply rotations, and save
        using (Document doc = new Document(inputPath))
        {
            foreach (var kvp in pageRotations)
            {
                int pageNumber = kvp.Key;
                int angle      = kvp.Value;

                // Ensure the page exists (Aspose.Pdf uses 1‑based indexing)
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                {
                    Console.WriteLine($"Skipping page {pageNumber}: out of range.");
                    continue;
                }

                // Convert the integer angle to the corresponding Rotation enum value
                // Page.IntToRotation handles 0, 90, 180, 270 degrees.
                Rotation rotationEnum = Page.IntToRotation(angle);

                // Apply the rotation to the page
                doc.Pages[pageNumber].Rotate = rotationEnum;
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}