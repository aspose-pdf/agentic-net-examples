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
        const int pageNumber = 1; // page to modify (1‑based index)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the page editor and bind the source PDF
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Restrict editing to the desired page
        editor.ProcessPages = new int[] { pageNumber };

        // Preserve existing transition settings (if any)
        int existingTransition = editor.TransitionType;
        int existingDuration = editor.TransitionDuration;

        // Apply 180° rotation
        editor.Rotation = 180;

        // Re‑apply the preserved transition settings
        editor.TransitionType = existingTransition;
        editor.TransitionDuration = existingDuration;

        // Commit changes and save the result
        editor.ApplyChanges();
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Page {pageNumber} rotated 180° and saved to '{outputPath}'.");
    }
}