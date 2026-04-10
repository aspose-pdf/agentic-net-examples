using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfPageEditor is a facade that allows page‑level editing.
        // It implements IDisposable, so we wrap it in a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPdf);

            // Restrict editing to page 5 only (pages are 1‑based).
            editor.ProcessPages = new int[] { 5 };

            // Set the display duration for the selected page to 5 seconds.
            editor.DisplayDuration = 5;

            // Apply the changes to the document.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Page 5 display duration set to 5 seconds. Saved as '{outputPdf}'.");
    }
}