using System;
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

        // Define the page range (inclusive) for which the transition duration will be set.
        int startPage = 2; // first page in the range (1‑based)
        int endPage   = 5; // last page in the range (1‑based)

        // Build an array containing all page numbers in the range.
        int[] pages = new int[endPage - startPage + 1];
        for (int i = 0; i < pages.Length; i++)
            pages[i] = startPage + i;

        // Use the PdfPageEditor facade to edit page properties.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Specify which pages to edit.
            editor.ProcessPages = pages;

            // Set the transition duration to 1 second for the selected pages.
            editor.TransitionDuration = 1;

            // Apply the changes to the document.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Transition duration set to 1 second for pages {startPage}-{endPage} and saved to '{outputPath}'.");
    }
}