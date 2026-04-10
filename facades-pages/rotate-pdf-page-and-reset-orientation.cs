using System;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath   = "input.pdf";
        const string rotatedPath = "rotated.pdf";
        const string resetPath   = "reset.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        int originalRotation = 0; // will hold the page's initial rotation

        // ---------- Rotate page 1 to 90 degrees ----------
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Remember the original rotation of page 1
            originalRotation = editor.GetPageRotation(1);

            // Set rotation for page 1 (90 degrees)
            editor.PageRotations = new Dictionary<int, int> { { 1, 90 } };

            // Apply the rotation change
            editor.ApplyChanges();

            // Save the rotated document
            editor.Save(rotatedPath);
        }

        // ---------- Reset page 1 back to its original rotation ----------
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the previously rotated PDF
            editor.BindPdf(rotatedPath);

            // Restore the original rotation for page 1
            editor.PageRotations = new Dictionary<int, int> { { 1, originalRotation } };

            // Apply the reset change
            editor.ApplyChanges();

            // Save the document with the original orientation restored
            editor.Save(resetPath);
        }

        Console.WriteLine($"Rotation applied and saved to '{rotatedPath}'.");
        Console.WriteLine($"Original orientation restored and saved to '{resetPath}'.");
    }
}