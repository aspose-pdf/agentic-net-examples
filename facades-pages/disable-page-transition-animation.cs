using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least six pages
            if (doc.Pages.Count >= 6)
            {
                // Disable transition animation on page 6 using PdfPageEditor (the API that
                // provides transition support). TransitionType = 0 corresponds to "None"
                // and TransitionDuration = 0 seconds disables any animation.
                using (PdfPageEditor editor = new PdfPageEditor(doc))
                {
                    editor.ProcessPages = new int[] { 6 }; // 1‑based page index
                    editor.TransitionType = 0;            // None
                    editor.TransitionDuration = 0;        // 0 seconds
                    editor.ApplyChanges();
                }
            }
            else
            {
                Console.Error.WriteLine("The document contains fewer than 6 pages.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Transition disabled on page 6. Saved to '{outputPath}'.");
    }
}
