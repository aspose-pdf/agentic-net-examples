using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_durations.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (Facade) to edit page properties.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF file.
            editor.BindPdf(inputPath);

            // Get total number of pages (1‑based indexing).
            int pageCount = editor.Document.Pages.Count;

            // Set a different display duration for each page.
            for (int i = 1; i <= pageCount; i++)
            {
                // Restrict editing to the current page.
                editor.ProcessPages = new int[] { i };

                // Set duration equal to the page index (seconds).
                editor.DisplayDuration = i;

                // Apply the change to the selected page.
                editor.ApplyChanges();
            }

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with per‑page display durations to '{outputPath}'.");
    }
}