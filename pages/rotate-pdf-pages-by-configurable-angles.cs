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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Configuration: page number (1‑based) -> rotation angle in degrees (must be 0, 90, 180, 270)
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
                int pageNumber = kvp.Key;   // 1‑based index
                int angle      = kvp.Value; // degrees

                // Ensure the page exists
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                {
                    Console.Error.WriteLine($"Page {pageNumber} is out of range. Skipping.");
                    continue;
                }

                // Convert integer angle to the Rotation enum and assign
                doc.Pages[pageNumber].Rotate = Page.IntToRotation(angle);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}