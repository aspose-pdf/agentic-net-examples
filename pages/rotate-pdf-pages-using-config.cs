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

        // Configuration: page number (1‑based) -> rotation angle (degrees, multiples of 90)
        var pageRotations = new Dictionary<int, int>
        {
            { 1, 90 },
            { 2, 180 },
            { 3, 270 }   // add more entries as needed
        };

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over the configuration dictionary
            foreach (var kvp in pageRotations)
            {
                int pageNumber = kvp.Key;   // 1‑based page index
                int angle      = kvp.Value; // rotation angle (must be 0, 90, 180, 270)

                // Ensure the page exists (pages are 1‑based)
                if (pageNumber >= 1 && pageNumber <= doc.Pages.Count)
                {
                    // Convert the integer angle to the Rotation enum and assign it
                    doc.Pages[pageNumber].Rotate = (Rotation)angle;
                }
                else
                {
                    Console.WriteLine($"Warning: Page {pageNumber} does not exist in the document.");
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}
