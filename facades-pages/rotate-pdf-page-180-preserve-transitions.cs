using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated.pdf";
        const int pageNumber = 1; // 1‑based page index to rotate

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            // -----------------------------------------------------------------
            // Verify that the target page already has a transition effect.
            // In recent Aspose.Pdf versions the Page class does not expose a
            // Transition property. The transition can be inspected/modified via
            // PdfPageEditor using the integer based TransitionType property.
            // -----------------------------------------------------------------
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                editor.ProcessPages = new int[] { pageNumber };
                // TransitionType == 0 means "No transition" in the library.
                if (editor.TransitionType == 0)
                    Console.WriteLine("Warning: page has no transition effect.");
                else
                    Console.WriteLine($"Existing transition type (int value): {editor.TransitionType}");
            }

            // -----------------------------------------------------------------
            // Rotate the page 180 degrees. The Rotate property of Page uses the
            // Rotation enum (on0, on90, on180, on270).
            // -----------------------------------------------------------------
            doc.Pages[pageNumber].Rotate = Rotation.on180;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page {pageNumber} rotated 180° and saved to '{outputPath}'.");
    }
}
