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

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor bound to the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // -----------------------------------------------------------------
                // Page 1 – Fade effect (implemented as DISSOLVE transition)
                // -----------------------------------------------------------------
                editor.ProcessPages = new int[] { 1 };               // edit only page 1
                editor.TransitionType = PdfPageEditor.DISSOLVE;     // Fade/Dissolve effect
                editor.TransitionDuration = 2;                       // duration in seconds
                editor.ApplyChanges();

                // -----------------------------------------------------------------
                // Page 2 – BoxOut effect (OUTBOX transition)
                // -----------------------------------------------------------------
                editor.ProcessPages = new int[] { 2 };
                editor.TransitionType = PdfPageEditor.OUTBOX;       // BoxOut effect
                editor.TransitionDuration = 2;
                editor.ApplyChanges();

                // -----------------------------------------------------------------
                // Page 3 – Cover effect (INBOX transition)
                // -----------------------------------------------------------------
                editor.ProcessPages = new int[] { 3 };
                editor.TransitionType = PdfPageEditor.INBOX;        // Cover effect
                editor.TransitionDuration = 2;
                editor.ApplyChanges();

                // Save the modified PDF. The Save method of PdfPageEditor writes the
                // result document to the specified file path.
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with page transitions saved to '{outputPath}'.");
    }
}