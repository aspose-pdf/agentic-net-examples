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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Bind the editor to the document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // ----- Page 1: Fade effect (DISSOLVE) -----
                editor.ProcessPages = new int[] { 1 };               // target page (1‑based)
                editor.TransitionType = PdfPageEditor.DISSOLVE;     // Fade transition
                editor.TransitionDuration = 2;                       // duration in seconds
                editor.ApplyChanges();

                // ----- Page 2: BoxOut effect (OUTBOX) -----
                editor.ProcessPages = new int[] { 2 };
                editor.TransitionType = PdfPageEditor.OUTBOX;       // BoxOut transition
                editor.TransitionDuration = 2;
                editor.ApplyChanges();

                // ----- Page 3: Cover effect (INBOX) -----
                editor.ProcessPages = new int[] { 3 };
                editor.TransitionType = PdfPageEditor.INBOX;        // Cover transition
                editor.TransitionDuration = 2;
                editor.ApplyChanges();

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Transitions applied and saved to '{outputPath}'.");
    }
}