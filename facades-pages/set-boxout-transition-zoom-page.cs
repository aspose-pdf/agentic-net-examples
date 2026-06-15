using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade
            PdfPageEditor editor = new PdfPageEditor();

            // Bind the loaded document to the editor
            editor.BindPdf(doc);

            // Apply changes only to page 2 (1‑based indexing)
            editor.ProcessPages = new int[] { 2 };

            // Set transition type to BoxOut (OUTBOX constant)
            editor.TransitionType = PdfPageEditor.OUTBOX;

            // Set zoom coefficient to 1.3 (130%) – use a float literal
            editor.Zoom = 1.3f;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);

            // Release resources held by the editor
            editor.Close();
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
