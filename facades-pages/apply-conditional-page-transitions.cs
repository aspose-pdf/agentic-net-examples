using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_transitions.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor facade bound to the document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Iterate through all pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    // Determine a transition type based on simple content heuristics
                    // Example: if the page contains any images use a "Blind Horizontal" transition,
                    // otherwise use a "Wipe Top‑Bottom" transition.
                    Page page = doc.Pages[pageNum];
                    int transition;

                    if (page.Resources.Images.Count > 0)
                    {
                        // Horizontal blinds transition
                        transition = PdfPageEditor.BLINDH;
                    }
                    else
                    {
                        // Top‑Bottom wipe transition
                        transition = PdfPageEditor.TBWIPE;
                    }

                    // Apply the transition to the current page
                    editor.ProcessPages = new int[] { pageNum };
                    editor.TransitionType = transition;
                    editor.TransitionDuration = 2; // seconds (optional)

                    // Commit changes for this page
                    editor.ApplyChanges();
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with page transitions: {outputPath}");
    }
}