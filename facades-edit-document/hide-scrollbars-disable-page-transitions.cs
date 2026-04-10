using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string tempPath   = "temp_viewer_pref.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // ---------- Step 1: Hide UI elements (scrollbars, toolbars, etc.) ----------
        PdfContentEditor contentEditor = new PdfContentEditor();
        contentEditor.BindPdf(inputPath);
        // HideWindowUI hides scrollbars and other UI controls.
        contentEditor.ChangeViewerPreference(ViewerPreference.HideWindowUI);
        contentEditor.Save(tempPath);
        contentEditor.Close();

        // ---------- Step 2: Disable page transition effects ----------
        PdfPageEditor pageEditor = new PdfPageEditor();
        pageEditor.BindPdf(tempPath);
        // Setting duration to 0 removes any transition animation.
        pageEditor.TransitionDuration = 0;
        // TransitionType = 0 (no transition). The constants are defined in PdfPageEditor.
        pageEditor.TransitionType = 0;
        pageEditor.ApplyChanges();
        pageEditor.Save(outputPath);
        pageEditor.Close();

        // Clean up temporary file.
        try { File.Delete(tempPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}