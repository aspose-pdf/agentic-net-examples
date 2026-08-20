using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Edit page properties using PdfPageEditor facade
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Target only page 2 (Aspose.Pdf uses 1‑based indexing)
            editor.ProcessPages = new int[] { 2 };

            // Set transition effect to BoxOut (outward box)
            editor.TransitionType = PdfPageEditor.OUTBOX;

            // Set zoom factor to 1.3 (130%) – note the 'f' suffix for a float literal
            editor.Zoom = 1.3f;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}
