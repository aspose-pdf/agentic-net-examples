using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "odd_pages_transition.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF with PdfPageEditor (Facades API)
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Total pages (1‑based indexing)
            int pageCount = editor.Document.Pages.Count;

            // Gather odd‑numbered page indices
            List<int> oddPages = new List<int>();
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                if (pageNumber % 2 == 1) // odd page
                    oddPages.Add(pageNumber);
            }

            if (oddPages.Count > 0)
            {
                // Apply transition to the collected odd pages
                editor.ProcessPages = oddPages.ToArray();
                // TransitionType is an int property; 5 corresponds to Split transition
                editor.TransitionType = 5;
                editor.TransitionDuration = 1; // seconds
                editor.ApplyChanges();
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with transitions on odd pages: {outputPath}");
    }
}
