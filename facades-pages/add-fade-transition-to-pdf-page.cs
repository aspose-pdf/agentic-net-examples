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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, apply a Fade transition of 2 seconds to the first page, and save.
        Document doc = new Document(inputPath);
        // Use PdfPageEditor to set page transitions (Transition property is not available on Page in recent versions).
        using (PdfPageEditor editor = new PdfPageEditor(doc))
        {
            // Target the first page (1‑based index).
            editor.ProcessPages = new int[] { 1 };
            // TransitionType uses integer values; 5 corresponds to the Fade (Dissolve) transition in the current Aspose.Pdf version.
            editor.TransitionType = 5; // Fade transition
            editor.TransitionDuration = 2; // 2 seconds
            editor.ApplyChanges();
            editor.Save(outputPath);
        }

        Console.WriteLine($"Transition applied and saved to '{outputPath}'.");
    }
}