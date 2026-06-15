using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        // Configuration: page number (1‑based) -> rotation angle in degrees (0, 90, 180, 270)
        var pageRotations = new Dictionary<int, int>
        {
            { 1, 90 },
            { 2, 180 },
            { 3, 0 }   // example entries; adjust as needed
        };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over the configuration dictionary
            foreach (var kvp in pageRotations)
            {
                int pageNumber = kvp.Key;   // 1‑based page index
                int angle = kvp.Value;      // rotation angle in degrees

                // Validate page number
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                {
                    Console.Error.WriteLine($"Page {pageNumber} is out of range; skipping.");
                    continue;
                }

                // Convert integer angle to the Rotation enum (Aspose.Pdf.Rotation)
                Rotation rotation = Page.IntToRotation(angle);

                // Apply rotation to the specific page
                doc.Pages[pageNumber].Rotate = rotation;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}