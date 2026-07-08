using System;
using System.IO;
using System.Collections.Generic;
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

        // Initialize the PdfPageEditor facade
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Determine total pages (1‑based indexing)
            int totalPages = editor.GetPages();

            // Build a list of odd‑numbered page indices
            List<int> oddPages = new List<int>();
            for (int i = 1; i <= totalPages; i += 2)
                oddPages.Add(i);

            // Specify that only the odd pages should be edited
            editor.ProcessPages = oddPages.ToArray();

            // Choose a transition style (e.g., vertical blinds) and duration
            editor.TransitionType = PdfPageEditor.BLINDV;   // vertical blinds
            editor.TransitionDuration = 2;                 // duration in seconds

            // Apply the changes to the selected pages
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with transitions on odd pages: '{outputPath}'");
    }
}