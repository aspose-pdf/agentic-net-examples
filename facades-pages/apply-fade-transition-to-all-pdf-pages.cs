using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_fade.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);

        // Apply a Fade transition to every page using PdfPageEditor.
        // In recent Aspose.Pdf versions the transition is set via the integer
        // value of the TransitionType property (11 = Fade). The duration is
        // specified in seconds.
        using (PdfPageEditor editor = new PdfPageEditor(pdfDoc))
        {
            editor.TransitionType = 11;          // Fade transition
            editor.TransitionDuration = 2;      // 2‑second duration

            // Apply the transition to all pages (1‑based page numbers).
            int pageCount = pdfDoc.Pages.Count;
            int[] allPages = new int[pageCount];
            for (int i = 0; i < pageCount; i++)
                allPages[i] = i + 1;
            editor.ProcessPages = allPages;

            editor.ApplyChanges();
        }

        // Save the modified PDF
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Fade transition applied to all pages. Saved as '{outputPath}'.");
    }
}
