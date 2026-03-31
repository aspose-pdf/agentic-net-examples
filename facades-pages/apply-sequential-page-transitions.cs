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

        using (Document doc = new Document(inputPath))
        {
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Array of different transition types to apply sequentially
            int[] transitions = new int[]
            {
                PdfPageEditor.BLINDH,
                PdfPageEditor.BLINDV,
                PdfPageEditor.BTWIPE,
                PdfPageEditor.DGLITTER,
                PdfPageEditor.DISSOLVE,
                PdfPageEditor.INBOX,
                PdfPageEditor.LRGLITTER,
                PdfPageEditor.LRWIPE,
                PdfPageEditor.OUTBOX,
                PdfPageEditor.RLWIPE,
                PdfPageEditor.SPLITHIN,
                PdfPageEditor.SPLITHOUT,
                PdfPageEditor.SPLITVIN,
                PdfPageEditor.SPLITVOUT,
                PdfPageEditor.TBGLITTER,
                PdfPageEditor.TBWIPE
            };

            int pageCount = doc.Pages.Count;
            for (int i = 1; i <= pageCount; i++)
            {
                int transition = transitions[(i - 1) % transitions.Length];
                // ProcessPages expects an int[]; provide a single‑element array
                editor.ProcessPages = new int[] { i };
                editor.TransitionType = transition;
                // TransitionDuration and DisplayDuration are integer properties (seconds)
                editor.TransitionDuration = 1; // seconds
                editor.DisplayDuration = 2;   // seconds each page stays visible
                editor.ApplyChanges();
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with sequential transitions saved to '{outputPath}'.");
    }
}
