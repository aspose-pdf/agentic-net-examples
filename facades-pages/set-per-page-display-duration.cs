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

        // Obtain the total number of pages via Document (PdfPageEditor does not expose this).
        int pageCount;
        using (var doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Use PdfPageEditor to set a different DisplayDuration for each page.
        using (var editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            for (int i = 1; i <= pageCount; i++)
            {
                // Specify the page to edit (1‑based indexing).
                editor.ProcessPages = new[] { i };
                // Set the duration (in seconds) for the selected page.
                editor.DisplayDuration = i; // page 1 => 1 s, page 2 => 2 s, …
                // Apply the change before moving to the next page.
                editor.ApplyChanges();
            }

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Saved with updated display durations to '{outputPath}'.");
    }
}
