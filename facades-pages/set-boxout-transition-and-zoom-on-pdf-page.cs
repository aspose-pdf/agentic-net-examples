using System;
using System.IO;
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

        // Initialize the facade and bind the source PDF
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Apply changes only to page 2
            editor.ProcessPages = new int[] { 2 };

            // Set transition to BoxOut (OUTBOX constant) and zoom to 1.3 (float literal)
            editor.TransitionType = PdfPageEditor.OUTBOX;
            editor.Zoom = 1.3f; // <-- float literal required

            // Apply the modifications and save the result
            editor.ApplyChanges();
            editor.Save(outputPath);
        }

        Console.WriteLine($"Transition and zoom applied to page 2. Saved as '{outputPath}'.");
    }
}
