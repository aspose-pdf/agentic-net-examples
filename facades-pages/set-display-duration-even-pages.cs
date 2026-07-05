using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_even_duration.pdf";
        const int    duration   = 5; // seconds for even‑numbered pages

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the document to the editor
                editor.BindPdf(doc);

                // Build an array of even page numbers (1‑based indexing)
                List<int> evenPages = new List<int>();
                for (int i = 2; i <= doc.Pages.Count; i += 2)
                {
                    evenPages.Add(i);
                }

                // Specify which pages the editor should process
                editor.ProcessPages = evenPages.ToArray();

                // Set the display duration (in seconds) for the selected pages
                editor.DisplayDuration = duration;

                // Apply the changes to the document
                editor.ApplyChanges();
            }

            // Save the modified PDF (PDF format by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Even‑page display duration set to {duration}s and saved to '{outputPath}'.");
    }
}