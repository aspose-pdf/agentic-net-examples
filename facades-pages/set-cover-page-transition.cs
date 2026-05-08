using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Initialize PdfPageEditor with the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Apply changes only to page 4 (1‑based indexing)
                editor.ProcessPages = new int[] { 4 };

                // TransitionType enum does not exist; use the integer value that represents "Cover"
                // 4 corresponds to the Cover transition type.
                editor.TransitionType = 4;

                // Set transition duration to 1 second
                editor.TransitionDuration = 1;

                // Apply the changes to the document
                editor.ApplyChanges();

                // Persist the modified PDF
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"Transition applied to page 4 and saved as '{outputPdf}'.");
    }
}