using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_durations.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // PdfPageEditor works on a Document instance
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Iterate over pages (1‑based indexing)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    // Edit only the current page
                    editor.ProcessPages = new int[] { pageIndex };
                    // Set display duration: increase by 1 second per page
                    editor.DisplayDuration = pageIndex; // seconds
                    // Apply the change to the document
                    editor.ApplyChanges();
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with per‑page display durations to '{outputPath}'.");
    }
}