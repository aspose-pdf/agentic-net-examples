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
            // Initialize the PdfPageEditor facade with the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Iterate over all pages (1‑based indexing)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    // Set the page(s) to be edited – only the current page
                    editor.ProcessPages = new int[] { pageIndex };

                    // Set display duration: increase by one second per page
                    editor.DisplayDuration = pageIndex; // seconds

                    // Apply the change to the current page
                    editor.ApplyChanges();
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with per‑page display durations to '{outputPath}'.");
    }
}