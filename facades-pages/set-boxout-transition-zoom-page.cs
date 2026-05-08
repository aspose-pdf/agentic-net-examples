using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source PDF exists – create a simple two‑page PDF if it does not.
        if (!File.Exists(inputPath))
        {
            using (Document tempDoc = new Document())
            {
                // Add two blank pages so that page index 2 is valid.
                tempDoc.Pages.Add();
                tempDoc.Pages.Add();
                tempDoc.Save(inputPath);
            }
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Initialize the page editor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);                     // bind the document
                editor.ProcessPages = new int[] { 2 };   // target only page 2 (1‑based indexing)

                // Set transition to outward box (BoxOut) and zoom to 1.3×
                editor.TransitionType = PdfPageEditor.OUTBOX; // BoxOut transition
                editor.Zoom = 1.3f;                           // 1.0 = 100%
                // Optional: set a short duration (seconds) for the transition
                editor.TransitionDuration = 1;               // 1 second

                // Apply the changes and save the result
                editor.ApplyChanges();
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Page 2 saved with BoxOut transition and 1.3 zoom to '{outputPath}'.");
    }
}
