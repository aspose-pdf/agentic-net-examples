using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_zoomed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor to modify page zoom
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Determine total number of pages
            int pageCount = editor.GetPages();

            // Collect odd‑numbered page indices (1‑based)
            List<int> oddPages = new List<int>();
            for (int i = 1; i <= pageCount; i += 2)
                oddPages.Add(i);

            // Specify that only the odd pages should be processed
            editor.ProcessPages = oddPages.ToArray();

            // Set zoom factor to 1.2 (120%)
            editor.Zoom = 1.2f;

            // Apply the changes to the selected pages
            editor.ApplyChanges();

            // Save the modified document
            editor.Save(outputPath);

            // Close the editor (optional, as using will dispose)
            editor.Close();
        }

        Console.WriteLine($"Zoom applied to odd pages. Saved as '{outputPath}'.");
    }
}