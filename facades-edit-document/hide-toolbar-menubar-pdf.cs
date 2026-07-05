using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade API for viewer preferences

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF
        const string outputPath = "minimal_ui.pdf"; // result PDF with hidden UI

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF into the facade, change viewer preferences, and save.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Hide the toolbar and the menu bar.
        editor.ChangeViewerPreference(ViewerPreference.HideToolbar);
        editor.ChangeViewerPreference(ViewerPreference.HideMenubar);

        // Persist the changes.
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Viewer preferences updated. Output saved to '{outputPath}'.");
    }
}