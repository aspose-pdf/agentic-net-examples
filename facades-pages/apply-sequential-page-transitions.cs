using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_transitions.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create the PdfPageEditor facade (lifecycle rule: use provided constructor)
            PdfPageEditor editor = new PdfPageEditor();

            // Bind the document to the editor
            editor.BindPdf(doc);

            // Define a set of transition constants to apply sequentially
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

            int pageCount = doc.Pages.Count; // page indexing is 1‑based (rule: page-indexing-one-based)

            // Apply a different transition to each page
            for (int i = 1; i <= pageCount; i++)
            {
                // Choose transition for this page (wrap around if more pages than transitions)
                int transition = transitions[(i - 1) % transitions.Length];

                // Specify the page to edit
                editor.ProcessPages = new int[] { i };

                // Set transition type and duration (seconds)
                editor.TransitionType = transition;
                editor.TransitionDuration = 2;

                // Apply the changes to the specified page
                editor.ApplyChanges();
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with page transitions saved to '{outputPath}'.");
    }
}