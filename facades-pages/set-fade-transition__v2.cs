using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int transitionDurationSeconds = 2;
        // Fade transition is represented by the DISSOLVE constant in PdfPageEditor
        const int fadeTransitionType = PdfPageEditor.DISSOLVE;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPath);

            // Apply the transition to all pages (default ProcessPages applies to every page)
            editor.TransitionType = fadeTransitionType;
            editor.TransitionDuration = transitionDurationSeconds;

            // Commit the changes
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Transition applied and saved to '{outputPath}'.");
    }
}