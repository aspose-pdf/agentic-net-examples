using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_transitions.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor facade and bind the loaded document
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);

                // Example: apply different transition effects to the first three pages
                // Transition types are defined as integer constants in PdfPageEditor.
                // Fade  -> DISSOLVE (page dissolves)
                // BoxOut -> INBOX (inward box)
                // Cover  -> OUTBOX (outward box)

                // Page 1 – Fade effect
                editor.ProcessPages = new int[] { 1 };          // target page number(s)
                editor.TransitionType = PdfPageEditor.DISSOLVE; // Fade (dissolve)
                editor.TransitionDuration = 2;                  // duration in seconds
                editor.ApplyChanges();

                // Page 2 – BoxOut effect
                editor.ProcessPages = new int[] { 2 };
                editor.TransitionType = PdfPageEditor.INBOX;   // BoxOut (inward box)
                editor.TransitionDuration = 2;
                editor.ApplyChanges();

                // Page 3 – Cover effect
                editor.ProcessPages = new int[] { 3 };
                editor.TransitionType = PdfPageEditor.OUTBOX;  // Cover (outward box)
                editor.TransitionDuration = 2;
                editor.ApplyChanges();

                // If you want the same transition for all pages, you can omit ProcessPages
                // and set the properties once, then call ApplyChanges().
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with transitions to '{outputPath}'.");
    }
}