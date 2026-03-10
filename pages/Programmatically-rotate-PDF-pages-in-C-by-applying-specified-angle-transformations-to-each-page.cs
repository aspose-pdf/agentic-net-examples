using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        // Example: rotation angle (in degrees) for each page.
        // If the array has fewer elements than pages, remaining pages stay unrotated (0°).
        int[] pageRotations = new int[] { 0, 90, 180, 270 }; // Page 1 → 0°, Page 2 → 90°, etc.

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement).
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Determine the desired rotation for the current page.
                int angle = (i - 1) < pageRotations.Length ? pageRotations[i - 1] : 0;

                // Convert integer degrees to the Rotation enum.
                Rotation rotation = Page.IntToRotation(angle);

                // Apply the rotation to the page.
                doc.Pages[i].Rotate = rotation;
            }

            // Save the modified document as PDF (no SaveOptions needed for PDF output).
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}