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
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            PdfPageEditor editor = new PdfPageEditor(doc);
            // Edit only page 2 (1‑based indexing)
            editor.ProcessPages = new int[] { 2 };
            // Set transition type to BoxOut (outward box)
            editor.TransitionType = PdfPageEditor.OUTBOX;
            // Set zoom factor to 1.3 (130%) – float literal required
            editor.Zoom = 1.3f;
            // Apply the changes to the document
            editor.ApplyChanges();
            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine("Transition and zoom applied to page 2, saved as " + outputPath);
    }
}
