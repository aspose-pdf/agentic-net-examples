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
        const string outputPath = "output.pdf";
        const int    evenPageDurationSeconds = 5; // duration for even‑numbered pages

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor facade bound to the loaded document
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Collect all even‑numbered page indices (1‑based indexing)
            List<int> evenPages = new List<int>();
            for (int i = 2; i <= doc.Pages.Count; i += 2)
                evenPages.Add(i);

            // Specify which pages the editor should affect
            editor.ProcessPages = evenPages.ToArray();

            // Set the display duration (in seconds) for the selected pages
            editor.DisplayDuration = evenPageDurationSeconds;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Even‑page durations set and saved to '{outputPath}'.");
    }
}