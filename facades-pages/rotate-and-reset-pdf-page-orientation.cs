using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string rotatedPath = "rotated.pdf";
        const string restoredPath = "restored.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // ---------- Rotate page 1 ----------
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the document to the editor
                editor.BindPdf(doc);

                // Set rotation for page 1 (90 degrees clockwise)
                editor.PageRotations = new System.Collections.Generic.Dictionary<int, int>
                {
                    { 1, 90 } // key = page number (1‑based), value = rotation in degrees
                };

                // Apply the rotation change
                editor.ApplyChanges();

                // Save the rotated document
                doc.Save(rotatedPath);
            }

            // ---------- Reset rotation to original ----------
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the same document (now rotated) to the editor
                editor.BindPdf(doc);

                // Reset rotation for page 1 to 0 degrees
                editor.PageRotations = new System.Collections.Generic.Dictionary<int, int>
                {
                    { 1, 0 }
                };

                // Apply the reset change
                editor.ApplyChanges();

                // Save the restored document
                doc.Save(restoredPath);
            }
        }

        Console.WriteLine($"Rotated PDF saved to '{rotatedPath}'.");
        Console.WriteLine($"Restored PDF saved to '{restoredPath}'.");
    }
}