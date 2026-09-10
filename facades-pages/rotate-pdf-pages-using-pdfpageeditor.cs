using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Allowed rotation angles in degrees.
    private static readonly int[] AllowedAngles = { 0, 90, 180, 270 };

    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        // Example rotation value – in real scenarios this could come from user input.
        int rotation = 90;

        // Validate the rotation before applying it.
        if (!IsValidRotation(rotation))
        {
            Console.Error.WriteLine($"Invalid rotation value: {rotation}. Allowed values are 0, 90, 180, 270.");
            return;
        }

        // Ensure the source file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (a Facades class) to load, rotate, and save the PDF.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document.
            editor.BindPdf(inputPath);

            // Apply the validated rotation to all pages.
            editor.Rotation = rotation;

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}' with rotation {rotation} degrees.");
    }

    // Helper method to check if the rotation angle is one of the allowed values.
    private static bool IsValidRotation(int angle)
    {
        foreach (int allowed in AllowedAngles)
        {
            if (angle == allowed)
                return true;
        }
        return false;
    }
}