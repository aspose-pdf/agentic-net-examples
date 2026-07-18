using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath      = "sample.pdf";
        const string rotatedPath    = "sample_rotated.pdf";
        const string restoredPath   = "sample_restored.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor to modify page rotation.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Store original rotation of the first page (pages are 1‑based).
            int originalRotation = editor.GetPageRotation(1);

            // ---------- Rotate ----------
            // Set rotation for all pages (0, 90, 180, 270). Here we rotate 90°.
            editor.Rotation = 90;
            editor.ApplyChanges();               // Apply the rotation.
            editor.Save(rotatedPath);            // Save the rotated document.
            Console.WriteLine($"Rotated PDF saved to '{rotatedPath}'.");

            // ---------- Reset ----------
            // Restore the original rotation.
            editor.Rotation = originalRotation;   // Typically 0.
            editor.ApplyChanges();               // Apply the reset.
            editor.Save(restoredPath);           // Save the restored document.
            Console.WriteLine($"Restored PDF saved to '{restoredPath}'.");
        }
    }
}