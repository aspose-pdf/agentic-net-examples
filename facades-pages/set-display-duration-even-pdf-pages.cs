using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_even_duration.pdf";
        const int    durationSec = 5; // seconds for even pages

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Prepare list of even page numbers (1‑based indexing)
            List<int> evenPages = new List<int>();
            for (int i = 2; i <= doc.Pages.Count; i += 2)
                evenPages.Add(i);

            // If there are no even pages, just save the original document
            if (evenPages.Count == 0)
            {
                doc.Save(outputPath);
                Console.WriteLine($"No even pages. Document saved unchanged to '{outputPath}'.");
                return;
            }

            // Edit page properties using PdfPageEditor
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);                     // bind the document
                editor.ProcessPages = evenPages.ToArray(); // target only even pages
                editor.DisplayDuration = durationSec;    // set desired duration (seconds)
                editor.ApplyChanges();                   // apply the changes
            }

            // Save the modified PDF
            doc.Save(outputPath);
            Console.WriteLine($"Even‑page display duration set to {durationSec}s. Saved to '{outputPath}'.");
        }
    }
}