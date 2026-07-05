using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // <-- added for TextFragment

class Program
{
    static void Main()
    {
        const string inputPath   = "input.pdf";
        const string rotatedPath = "rotated.pdf";
        const string resetPath   = "reset.pdf";

        // -------------------------------------------------
        // Ensure a source PDF exists – create a simple one if missing
        // -------------------------------------------------
        if (!File.Exists(inputPath))
        {
            using (Document tempDoc = new Document())
            {
                Page page = tempDoc.Pages.Add();
                page.Paragraphs.Add(new TextFragment("Sample PDF for rotation demo."));
                tempDoc.Save(inputPath);
            }
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor with the loaded document
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Retrieve the original rotation of the first page (pages are 1‑based)
            int originalRotation = editor.GetPageRotation(1);

            // -------------------------------------------------
            // Rotate all pages by 90 degrees
            // -------------------------------------------------
            editor.Rotation = 90;          // valid values: 0, 90, 180, 270
            editor.ApplyChanges();         // apply the rotation to the document
            doc.Save(rotatedPath);         // save the rotated version

            // -------------------------------------------------
            // Reset the rotation back to the original orientation
            // -------------------------------------------------
            editor.Rotation = originalRotation; // restore original rotation
            editor.ApplyChanges();              // re‑apply changes
            doc.Save(resetPath);                // save the restored version
        }

        Console.WriteLine($"Rotated PDF saved to '{rotatedPath}'.");
        Console.WriteLine($"Reset PDF saved to '{resetPath}'.");
    }
}
