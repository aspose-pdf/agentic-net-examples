using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath   = "sample.pdf";
        const string rotatedPath = "sample_rotated.pdf";
        const string resetPath   = "sample_reset.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        int originalRotation = 0;

        // Rotate the first page to 90 degrees and save the result
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);                         // Load the PDF
            originalRotation = editor.GetPageRotation(1);      // Store original rotation (1‑based index)
            editor.Rotation = 90;                              // Set desired rotation (must be 0,90,180,270)
            editor.ApplyChanges();                             // Apply the rotation
            editor.Save(rotatedPath);                          // Save the rotated PDF
        }

        // Reset the page back to its original orientation and save again
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(rotatedPath);       // Load the previously rotated PDF
            editor.Rotation = originalRotation; // Restore original rotation
            editor.ApplyChanges();              // Apply the reset
            editor.Save(resetPath);             // Save the PDF with original orientation restored
        }

        Console.WriteLine($"Rotation applied and saved to '{rotatedPath}'.");
        Console.WriteLine($"Original orientation restored and saved to '{resetPath}'.");
    }
}