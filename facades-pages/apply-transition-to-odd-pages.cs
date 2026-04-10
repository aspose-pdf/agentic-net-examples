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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor bound to the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Determine odd‑numbered pages (1‑based indexing)
                int pageCount = doc.Pages.Count;
                int[] oddPages = new int[(pageCount + 1) / 2];
                int idx = 0;
                for (int i = 1; i <= pageCount; i += 2)
                {
                    oddPages[idx++] = i;
                }

                // Apply the transition only to the odd pages
                editor.ProcessPages = oddPages;                     // pages to edit
                editor.TransitionType = PdfPageEditor.BLINDV;       // example transition style
                editor.TransitionDuration = 2;                     // duration in seconds (int required)

                // Commit the changes to the document
                editor.ApplyChanges();
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with transitions on odd pages: {outputPath}");
    }
}
