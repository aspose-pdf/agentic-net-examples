using System;
using System.IO;
using Aspose.Pdf;               // ViewerPreference enum
using Aspose.Pdf.Facades;      // PdfContentEditor

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor implements SaveableFacade, which supports IDisposable.
        // Use a using block to ensure proper resource cleanup.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document into the editor.
            editor.BindPdf(inputPath);

            // Change a viewer preference, e.g., hide the menu bar.
            editor.ChangeViewerPreference(ViewerPreference.HideMenubar);

            // Save the modified PDF to a new file path.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}