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
            // Bind the document to PdfPageEditor
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Iterate over all pages (1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Edit only the current page
                    editor.ProcessPages = new int[] { i };

                    // Choose a transition type based on page index modulo 3
                    int mod = i % 3;
                    if (mod == 1)
                        editor.TransitionType = PdfPageEditor.BLINDH;      // vertical blinds
                    else if (mod == 2)
                        editor.TransitionType = PdfPageEditor.BLINDV;      // horizontal blinds
                    else
                        editor.TransitionType = PdfPageEditor.DISSOLVE;    // dissolve effect

                    // Optional: set transition duration (seconds)
                    editor.TransitionDuration = 2;

                    // Apply the changes to the current page
                    editor.ApplyChanges();
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Transitions applied and saved to '{outputPath}'.");
    }
}