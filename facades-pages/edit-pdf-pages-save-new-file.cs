using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // original PDF (unchanged)
        const string outputPath = "edited_copy.pdf";    // new file with edits

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the original PDF to the facade. The facade works on an in‑memory
        // representation, so the source file stays untouched.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Example edit: change the zoom factor for all pages.
            editor.Zoom = 0.75f;

            // Save the edited document to a separate file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved as '{outputPath}'. Original file remains unchanged.");
    }
}