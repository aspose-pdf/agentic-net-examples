using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_transition.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document to obtain the total page count
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;

            // Build an array containing only odd‑numbered page indices (1‑based)
            int[] oddPages = new int[(pageCount + 1) / 2];
            int idx = 0;
            for (int i = 1; i <= pageCount; i += 2)
            {
                oddPages[idx++] = i;
            }

            // Create a PdfPageEditor, bind the source PDF, and configure the transition
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(inputPath);               // Initialize the facade with the source file
                editor.ProcessPages = oddPages;          // Apply changes only to odd pages
                editor.TransitionType = PdfPageEditor.BLINDH; // Example transition style (horizontal blinds)
                editor.TransitionDuration = 2;           // Duration in seconds

                editor.ApplyChanges();                   // Commit the changes to the document
                editor.Save(outputPath);                 // Save the modified PDF
            }
        }

        Console.WriteLine($"Saved PDF with transitions to '{outputPath}'.");
    }
}