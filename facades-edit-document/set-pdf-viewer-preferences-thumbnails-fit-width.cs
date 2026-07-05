using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfContentEditor and ViewerPreference

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_prefs.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the facade, bind the source PDF, set viewer preferences, and save.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Combine flags: show thumbnails pane (PageModeUseThumbs) and fit window (FitWindow).
        int viewerPrefs = ViewerPreference.PageModeUseThumbs | ViewerPreference.FitWindow;
        editor.ChangeViewerPreference(viewerPrefs);

        // Save the modified PDF.
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"PDF saved with viewer preferences to '{outputPath}'.");
    }
}