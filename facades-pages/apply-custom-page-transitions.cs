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

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(doc);

                // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                int pageCount = doc.Pages.Count;
                for (int i = 1; i <= pageCount; i++)
                {
                    // Apply transition only to the current page
                    editor.ProcessPages = new int[] { i };

                    // Set a common transition duration (seconds)
                    editor.TransitionDuration = 2;

                    // Choose transition type based on page index
                    // Even pages: vertical blinds, odd pages: left‑right wipe
                    if (i % 2 == 0)
                    {
                        editor.TransitionType = PdfPageEditor.BLINDV; // vertical blinds
                    }
                    else
                    {
                        editor.TransitionType = PdfPageEditor.LRWIPE; // left‑right wipe
                    }

                    // Commit the changes for this page
                    editor.ApplyChanges();
                }

                // Save the modified PDF with the applied transitions
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Presentation PDF saved to '{outputPath}'.");
    }
}