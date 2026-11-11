using System;
using System.IO;
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

        // Use PdfPageEditor to set a page transition.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // 1‑based page numbers to which the transition will be applied.
            editor.ProcessPages = new int[] { 5 };

            // Transition type: Dissolve.
            // In newer Aspose.Pdf versions you can use the enum:
            // editor.TransitionType = (int)TransitionType.Dissolve;
            // For older versions the integer value (2) is used directly.
            editor.TransitionType = 2; // Dissolve

            // Duration of the transition in seconds.
            editor.TransitionDuration = 3;

            // Persist the changes.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Transition applied and saved to '{outputPath}'.");
    }
}
