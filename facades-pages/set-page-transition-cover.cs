using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfPageEditor implements IDisposable, so wrap it in a using block
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF to be edited
            editor.BindPdf(inputPdf);

            // Restrict editing to page 4 (Aspose.Pdf uses 1‑based indexing)
            editor.ProcessPages = new int[] { 4 };

            // Set the transition style.
            // The "Cover" transition corresponds to the integer value 0 in Aspose.Pdf.
            editor.TransitionType = 0;          // Cover transition
            editor.TransitionDuration = 1;      // Duration in seconds

            // Apply the changes and save the modified document
            editor.ApplyChanges();
            editor.Save(outputPdf);
            // editor.Close() is called automatically by the using statement
        }

        Console.WriteLine($"Transition applied and saved to '{outputPdf}'.");
    }
}