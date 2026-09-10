using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF files
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_renamed.pdf";

        // Translation dictionary: original bookmark title -> new title
        var translation = new Dictionary<string, string>
        {
            { "Chapter 1", "Capítulo 1" },
            { "Chapter 2", "Capítulo 2" },
            { "Conclusion", "Conclusión" }
            // Add more entries as needed
        };

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Use PdfBookmarkEditor (create‑load‑save lifecycle)
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPdf);

            // Rename each bookmark according to the dictionary
            foreach (var kvp in translation)
            {
                // ModifyBookmarks changes all bookmarks with the source title to the destination title
                editor.ModifyBookmarks(kvp.Key, kvp.Value);
            }

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmarks renamed and saved to '{outputPdf}'.");
    }
}