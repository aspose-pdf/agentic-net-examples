using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated.pdf";
        const int pageNumber = 1; // 1‑based page index
        const int rotationValue = 45; // Example of an invalid rotation (valid: 0, 90, 180, 270)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Validate rotation value – if invalid, fall back to 0°
                int validRotation = GetValidRotation(rotationValue, out bool isValid);
                if (!isValid)
                {
                    Console.Error.WriteLine($"Invalid rotation value ({rotationValue}). Using default 0°.");
                }

                // Apply rotation using the Document API (PdfPageEditor has no SetPageRotation method)
                try
                {
                    doc.Pages[pageNumber].Rotate = (Rotation)validRotation; // Rotation enum values are 0,90,180,270
                }
                catch (ArgumentException ex)
                {
                    Console.Error.WriteLine($"Failed to set rotation: {ex.Message}");
                }

                // Save the modified document (PDF format)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Document saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    private static int GetValidRotation(int value, out bool isValid)
    {
        // Allowed rotation angles for Aspose.Pdf are 0, 90, 180, 270 degrees
        var allowed = new HashSet<int> { 0, 90, 180, 270 };
        if (allowed.Contains(value))
        {
            isValid = true;
            return value;
        }
        isValid = false;
        return 0; // default rotation
    }
}
