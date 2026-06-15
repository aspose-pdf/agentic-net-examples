using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_transitions.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: using block)
        using (Document doc = new Document(inputPath))
        {
            // Initialize PdfPageEditor with the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Determine odd‑numbered pages (1‑based indexing)
                int[] oddPages = GetOddPageNumbers(doc.Pages.Count);

                // Apply the editor only to odd pages
                editor.ProcessPages = oddPages;

                // Choose a transition style (e.g., DISSOLVE)
                editor.TransitionType = PdfPageEditor.DISSOLVE;

                // Set transition duration in seconds
                editor.TransitionDuration = 2;

                // Apply the changes to the document
                editor.ApplyChanges();

                // Save the modified PDF (lifecycle rule)
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with transitions saved to '{outputPath}'.");
    }

    // Helper method to generate an array of odd page numbers (1‑based)
    static int[] GetOddPageNumbers(int totalPages)
    {
        int oddCount = (totalPages + 1) / 2;
        int[] odds = new int[oddCount];
        int idx = 0;
        for (int i = 1; i <= totalPages; i += 2)
        {
            odds[idx++] = i;
        }
        return odds;
    }
}