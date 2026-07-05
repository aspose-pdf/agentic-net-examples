using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_fade.pdf";

        // Verify the source file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Initialize the facade and bind the PDF document
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Set the transition effect to "fade" (implemented as DISSOLVE) and duration to 2 seconds
        editor.TransitionType = PdfPageEditor.DISSOLVE;   // fade/dissolve effect
        editor.TransitionDuration = 2;                    // seconds

        // By default all pages are processed; no need to set ProcessPages explicitly.
        // If you prefer to be explicit, you could assign an array of page numbers:
        // editor.ProcessPages = new int[200];
        // for (int i = 0; i < 200; i++) editor.ProcessPages[i] = i + 1;

        // Apply the changes to the document
        editor.ApplyChanges();

        // Save the modified PDF
        editor.Save(outputPath);

        // Release resources
        editor.Close();

        Console.WriteLine($"PDF with fade transition saved to '{outputPath}'.");
    }
}