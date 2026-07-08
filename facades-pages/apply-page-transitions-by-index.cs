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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor bound to the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
                {
                    // Determine which transition to apply based on (pageIndex‑1) % 3
                    int mod = (pageNumber - 1) % 3;
                    int transitionType;

                    switch (mod)
                    {
                        case 0:
                            transitionType = PdfPageEditor.BLINDH;   // Vertical blinds
                            break;
                        case 1:
                            transitionType = PdfPageEditor.BLINDV;   // Horizontal blinds
                            break;
                        default:
                            transitionType = PdfPageEditor.DISSOLVE; // Dissolve effect
                            break;
                    }

                    // Restrict editing to the current page only
                    editor.ProcessPages = new int[] { pageNumber };

                    // Apply the selected transition type and a fixed duration (seconds)
                    editor.TransitionType     = transitionType;
                    editor.TransitionDuration = 2; // 2‑second transition

                    // Commit changes for this page
                    editor.ApplyChanges();
                }
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Transitions applied and saved to '{outputPath}'.");
    }
}