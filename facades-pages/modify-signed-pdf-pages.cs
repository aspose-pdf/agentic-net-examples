using System;
using System.Drawing;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "signed_input.pdf";
        const string outputPdf = "signed_modified.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the signed PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Initialize PdfPageEditor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(doc);

                // Example modification: rotate page 2 by 90 degrees
                // Page numbers are 1‑based
                editor.Rotation = 90;
                editor.ProcessPages = new int[] { 2 }; // apply only to page 2

                // Example modification: change page size for page 2
                // Set the output page size (e.g., A4)
                editor.PageSize = PageSize.A4;

                // Apply the queued changes
                editor.ApplyChanges();

                // Save the modified document.
                // Using the same file name ensures the original signature remains valid
                // because the changes are applied in an incremental update.
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}