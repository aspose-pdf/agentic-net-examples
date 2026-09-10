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

        // Load the PDF document (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor and bind it to the loaded document
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);

            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Determine transition type based on page index modulo 3
                int mod = (i - 1) % 3;
                switch (mod)
                {
                    case 0:
                        editor.TransitionType = PdfPageEditor.BLINDH; // vertical blinds
                        break;
                    case 1:
                        editor.TransitionType = PdfPageEditor.BLINDV; // horizontal blinds
                        break;
                    case 2:
                        editor.TransitionType = PdfPageEditor.DISSOLVE; // dissolve effect
                        break;
                }

                // Apply the transition only to the current page
                editor.ProcessPages = new int[] { i };
                editor.ApplyChanges();
            }

            // Save the modified PDF (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with page transitions to '{outputPath}'.");
    }
}