using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_zoomed.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfPageEditor implements IDisposable, so wrap it in a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPdf);

            // Get total number of pages (1‑based indexing).
            int pageCount = editor.GetPages();

            // Apply a different zoom factor to each page.
            for (int i = 1; i <= pageCount; i++)
            {
                // Example: zoom starts at 0.5 and increases by 0.1 per page.
                float zoomFactor = 0.5f + (i - 1) * 0.1f;

                // Restrict editing to the current page only.
                editor.ProcessPages = new int[] { i };

                // Set the zoom coefficient for this page.
                editor.Zoom = zoomFactor;

                // Apply the changes to the page.
                editor.ApplyChanges();
            }

            // Save the modified document.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Zoomed PDF saved to '{outputPdf}'.");
    }
}