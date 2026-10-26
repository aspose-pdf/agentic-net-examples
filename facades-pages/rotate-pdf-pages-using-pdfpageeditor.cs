using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Rotation values for each page (1‑based index). Adjust the array length as needed.
        int[] rotations = new int[] { 0, 90, 180, 270 };

        // Validate that every rotation value is allowed.
        foreach (int angle in rotations)
        {
            if (!IsValidRotation(angle))
            {
                Console.Error.WriteLine($"Invalid rotation angle: {angle}. Allowed values are 0, 90, 180, 270.");
                return;
            }
        }

        // Use PdfPageEditor to apply per‑page rotations.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Build a dictionary where key = page number (1‑based) and value = rotation angle.
            var pageRotations = new Dictionary<int, int>();
            for (int i = 1; i <= rotations.Length; i++)
            {
                pageRotations[i] = rotations[i - 1];
            }
            editor.PageRotations = pageRotations;

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }

    // Helper to ensure only permitted rotation angles are used.
    static bool IsValidRotation(int angle) => angle == 0 || angle == 90 || angle == 180 || angle == 270;
}