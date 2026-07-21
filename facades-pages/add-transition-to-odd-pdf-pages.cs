using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_odd_transition.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine the total number of pages in the source PDF
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            int pageCount = doc.Pages.Count;

            // Build an array containing only the odd‑numbered page indices (1‑based)
            List<int> oddPages = new List<int>();
            for (int i = 1; i <= pageCount; i += 2)
            {
                oddPages.Add(i);
            }

            // Apply a transition effect to the odd pages using PdfPageEditor
            using (Aspose.Pdf.Facades.PdfPageEditor editor = new Aspose.Pdf.Facades.PdfPageEditor())
            {
                // Bind the source PDF file to the editor
                editor.BindPdf(inputPath);

                // Choose a transition style (e.g., vertical blinds) and set its duration (seconds)
                editor.TransitionType = Aspose.Pdf.Facades.PdfPageEditor.BLINDV;
                editor.TransitionDuration = 2;

                // Restrict the editor to process only the odd pages
                editor.ProcessPages = oddPages.ToArray();

                // Apply the changes and save the result
                editor.ApplyChanges();
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with transitions saved to '{outputPath}'.");
    }
}