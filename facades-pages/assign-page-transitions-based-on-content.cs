using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_transitions.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the source PDF document (lifecycle: load)
        using (Document doc = new Document(inputPdf))
        {
            // Create the PdfPageEditor facade (lifecycle: create)
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Determine a transition type based on simple content heuristics.
                // XObjects collection is not exposed in recent Aspose.Pdf versions, so we fall back to checking Forms.
                int transition;
                if (page.Resources.Images.Count > 0)
                {
                    // Pages containing images get a "Dissolve" effect
                    transition = PdfPageEditor.DISSOLVE;
                }
                else if (page.Resources.Forms != null && page.Resources.Forms.Count > 0)
                {
                    // Pages containing form XObjects (often tables, vector graphics) get a "Blind Horizontal" effect
                    transition = PdfPageEditor.BLINDH;
                }
                else
                {
                    // All other pages get a "Box Outward" effect
                    transition = PdfPageEditor.OUTBOX;
                }

                // Apply the transition only to the current page
                editor.ProcessPages = new int[] { pageNum };
                editor.TransitionType = transition;
                editor.TransitionDuration = 2; // seconds

                // Commit the change for this page
                editor.ApplyChanges();
            }

            // Save the modified PDF (lifecycle: save)
            editor.Save(outputPdf);
            editor.Close(); // optional cleanup
        }

        Console.WriteLine($"PDF saved with page transitions: {outputPdf}");
    }
}
