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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Bind the document to PdfPageEditor (facade for page editing)
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Determine transition type based on page index modulo 3
                    // Use three different built‑in transition constants
                    int transitionType;
                    switch (i % 3)
                    {
                        case 0:
                            transitionType = PdfPageEditor.BLINDH;   // Vertical blinds
                            break;
                        case 1:
                            transitionType = PdfPageEditor.BLINDV;   // Horizontal blinds
                            break;
                        default:
                            transitionType = PdfPageEditor.BTWIPE;   // Bottom‑Top wipe
                            break;
                    }

                    // Set the transition type for the current page
                    // (TransitionType is an int property; assign the constant)
                    editor.TransitionType = transitionType;

                    // Apply the change to the document.
                    // ApplyChanges writes the current settings to the bound document.
                    editor.ApplyChanges();
                }
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with page transitions to '{outputPath}'.");
    }
}