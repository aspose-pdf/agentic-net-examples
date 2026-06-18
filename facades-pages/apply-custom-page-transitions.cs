using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_transition.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Create the page editor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(doc);

                // Iterate over all pages (1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Choose a transition style based on the page index
                    int transition = (i % 3) switch
                    {
                        0 => PdfPageEditor.BLINDH,   // Vertical blinds
                        1 => PdfPageEditor.DISSOLVE, // Dissolve effect
                        _ => PdfPageEditor.TBWIPE    // Top‑bottom wipe
                    };

                    // Specify that only the current page should be edited
                    editor.ProcessPages = new int[] { i }; // int[] required by PdfPageEditor

                    // Apply the transition settings
                    editor.TransitionType = transition;
                    editor.TransitionDuration = 2; // duration in seconds (integer)

                    // Commit changes for this page
                    editor.ApplyChanges();
                }

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with custom transitions saved to '{outputPath}'.");
    }
}
