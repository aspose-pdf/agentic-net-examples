using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // ------------------------------------------------------------
        // 0. Ensure a source PDF exists (self‑contained example)
        // ------------------------------------------------------------
        if (!System.IO.File.Exists(inputPath))
        {
            // Create a minimal one‑page PDF so the rest of the demo can work.
            using (var placeholder = new Document())
            {
                placeholder.Pages.Add();
                placeholder.Save(inputPath);
            }
        }

        // ------------------------------------------------------------
        // 1. Hide UI elements (scrollbars, navigation controls, etc.)
        // ------------------------------------------------------------
        using (var viewerEditor = new PdfContentEditor())
        {
            viewerEditor.BindPdf(inputPath);
            // ViewerPreference.HideWindowUI hides scrollbars and other UI elements.
            viewerEditor.ChangeViewerPreference(ViewerPreference.HideWindowUI);
            viewerEditor.Save(outputPath);
        }

        // ------------------------------------------------------------
        // 2. Disable page transition effects (set to none)
        // ------------------------------------------------------------
        using (var pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(outputPath);
            // TransitionType = 0 means no transition.
            pageEditor.TransitionType = 0;
            // TransitionDuration = 0 removes any timing.
            pageEditor.TransitionDuration = 0;
            // Apply the changes to all pages.
            pageEditor.ApplyChanges();
            pageEditor.Save(outputPath);
        }

        Console.WriteLine($"Viewer preferences applied and transitions disabled. Output saved to '{outputPath}'.");
    }
}
