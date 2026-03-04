using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputMht   = "input.mht";   // source MHT file
        const string outputPdf  = "output.pdf";  // result PDF with transition

        if (!File.Exists(inputMht))
        {
            Console.Error.WriteLine($"File not found: {inputMht}");
            return;
        }

        // Load the MHT file into a PDF Document (Aspose.Pdf auto‑detects the format)
        using (Document doc = new Document(inputMht))
        {
            // Initialize the PdfPageEditor facade and bind the document
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);

            // Set a transition effect for all pages.
            // Example: vertical blinds transition (BLINDV constant) with a 2‑second duration.
            editor.TransitionType = PdfPageEditor.BLINDV;   // transition style
            editor.TransitionDuration = 2;                 // duration in seconds

            // Apply the changes to the document pages.
            editor.ApplyChanges();

            // Save the modified document as PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Transition applied and saved to '{outputPdf}'.");
    }
}