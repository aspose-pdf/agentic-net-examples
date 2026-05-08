using System;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath    = "input.pdf";
        const string rotatedPath  = "rotated.pdf";
        const string restoredPath = "restored.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (facade) to modify page rotation.
        // The facade implements IDisposable, so wrap it in a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document.
            editor.BindPdf(inputPath);

            // Retrieve the original rotation of the first page (1‑based indexing).
            int originalRotation = editor.GetPageRotation(1);

            // Rotate the first page to 90 degrees.
            editor.PageRotations = new Dictionary<int, int> { { 1, 90 } };
            editor.ApplyChanges();

            // Save the rotated version.
            editor.Save(rotatedPath);
            Console.WriteLine($"Rotated PDF saved to '{rotatedPath}'.");

            // Reset the rotation back to the original value.
            editor.PageRotations = new Dictionary<int, int> { { 1, originalRotation } };
            editor.ApplyChanges();

            // Save the restored (original orientation) version.
            editor.Save(restoredPath);
            Console.WriteLine($"Restored PDF saved to '{restoredPath}'.");
        }
    }
}