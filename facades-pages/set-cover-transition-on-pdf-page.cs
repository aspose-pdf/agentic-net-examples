using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor to apply a page transition
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Target page 4 (1‑based indexing)
            editor.ProcessPages = new int[] { 4 };

            // TransitionType is a property, not an enum. The integer value 4 represents the "Cover" transition.
            editor.TransitionType = 4;

            // Duration is expressed in seconds.
            editor.TransitionDuration = 1;

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Cover transition applied to page 4 with 1‑second duration and saved as '{outputPath}'.");
    }
}
