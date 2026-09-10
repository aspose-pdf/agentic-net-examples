using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const int    duration  = 5; // seconds for even‑numbered pages

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF to the facade
        PdfPageEditor editor = new PdfPageEditor();
        try
        {
            editor.BindPdf(inputPdf);

            // Determine even‑numbered pages (1‑based indexing)
            int pageCount = editor.GetPages(); // total pages in the document
            int[] evenPages = GetEvenPages(pageCount);

            // Restrict editing to the even pages only
            editor.ProcessPages = evenPages;

            // Set the display duration (in seconds) for the selected pages
            editor.DisplayDuration = duration;

            // Apply the changes and save the result
            editor.ApplyChanges();
            editor.Save(outputPdf);
        }
        finally
        {
            // PdfPageEditor does not implement IDisposable; just close the facade
            editor.Close();
        }

        Console.WriteLine($"Saved PDF with even‑page durations to '{outputPdf}'.");
    }

    // Helper to build an array of even page numbers (1‑based)
    private static int[] GetEvenPages(int totalPages)
    {
        int evenCount = totalPages / 2;
        int[] evens = new int[evenCount];
        int idx = 0;
        for (int i = 2; i <= totalPages; i += 2)
        {
            evens[idx++] = i;
        }
        return evens;
    }
}