using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor does not implement IDisposable, so we instantiate it directly.
        PdfContentEditor editor = new PdfContentEditor();

        // Load the PDF document.
        editor.BindPdf(inputPath);

        // Hide the toolbar and the menu bar using ViewerPreference flags.
        editor.ChangeViewerPreference(ViewerPreference.HideToolbar);
        editor.ChangeViewerPreference(ViewerPreference.HideMenubar);

        // Save the modified PDF.
        editor.Save(outputPath);
        editor.Close(); // Release internal resources.

        Console.WriteLine($"Viewer preferences applied. Saved to '{outputPath}'.");
    }
}