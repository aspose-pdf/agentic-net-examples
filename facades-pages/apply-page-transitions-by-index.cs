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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor and bind it to the loaded document
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);

                // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Choose a transition type based on page index modulo 3
                    int transition;
                    switch ((i - 1) % 3)
                    {
                        case 0:
                            transition = PdfPageEditor.BLINDH;   // Vertical blinds
                            break;
                        case 1:
                            transition = PdfPageEditor.BLINDV;   // Horizontal blinds
                            break;
                        default:
                            transition = PdfPageEditor.BTWIPE;   // Bottom‑Top wipe
                            break;
                    }

                    // Apply the transition only to the current page
                    editor.ProcessPages = new int[] { i };
                    editor.TransitionType = transition;
                    editor.TransitionDuration = 2; // optional: 2 seconds per transition
                    editor.ApplyChanges();
                }
            }

            // Save the modified PDF (PDF format does not require explicit SaveOptions)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with page transitions saved to '{outputPath}'.");
    }
}