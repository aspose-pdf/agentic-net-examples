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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Example mapping of page numbers to titles.
        var pageTitles = new Dictionary<int, string>
        {
            { 1, "Short" },
            { 2, "A much longer page title" },
            { 3, "Medium length title" }
        };

        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Get the title for the current page; default to empty string.
                pageTitles.TryGetValue(i, out string title);
                title ??= string.Empty;

                // Compute transition duration: 1 second per 10 characters, minimum 1 second.
                int duration = Math.Max(1, title.Length / 10);

                // Apply transition settings to the current page.
                using (PdfPageEditor editor = new PdfPageEditor(doc))
                {
                    editor.ProcessPages = new int[] { i };
                    // Transition type is an integer; 4 corresponds to the "Cover" transition.
                    editor.TransitionType = 4;
                    editor.TransitionDuration = duration;
                    editor.ApplyChanges();
                }
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with page transitions to '{outputPath}'.");
    }
}