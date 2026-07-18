using System;
using System.IO;
using Aspose.Pdf;                     // Document, Page, etc.
using Aspose.Pdf.Facades;            // PdfPageEditor

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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Bind the document to PdfPageEditor (facade API)
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                int pageCount = doc.Pages.Count; // 1‑based indexing

                // Apply a different transition to each page based on its index
                for (int i = 1; i <= pageCount; i++)
                {
                    // Restrict editing to the current page only
                    editor.ProcessPages = new int[] { i };

                    // Choose a transition style per page (example: alternate between two styles)
                    if (i % 2 == 0)
                        editor.TransitionType = PdfPageEditor.BLINDH;      // vertical blinds
                    else
                        editor.TransitionType = PdfPageEditor.DISSOLVE;   // dissolve effect

                    // Set transition duration (seconds)
                    editor.TransitionDuration = 2;

                    // Apply the changes for the current page
                    editor.ApplyChanges();
                }
            }

            // Save the modified PDF (lifecycle rule: use Save without extra options for PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom transitions to '{outputPath}'.");
    }
}