using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a PdfPageEditor bound to the loaded document
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Edit only page 4 (pages are 1‑based)
            editor.ProcessPages = new int[] { 4 };

            // Set the transition type to "Cover". The exact integer value for Cover
            // is not exposed as a named constant, so we assign the known value directly.
            // (If a named constant becomes available, replace the literal with it.)
            editor.TransitionType = 0; // Cover transition

            // Set the transition duration to 1 second
            editor.TransitionDuration = 1;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Transition applied and saved to '{outputPdf}'.");
    }
}