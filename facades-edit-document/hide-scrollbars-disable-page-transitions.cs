using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // ---------------------------------------------------------------------
        // 1. Create a minimal PDF so the sandbox has a file to work with.
        // ---------------------------------------------------------------------
        using (Document seed = new Document())
        {
            // Add a single blank page (required for most PDF operations).
            seed.Pages.Add();
            seed.Save(inputPath);
        }

        // ---------------------------------------------------------------------
        // 2. Hide UI elements (scrollbars, toolbars, menu bar, etc.) using
        //    ViewerPreference flags. The enum is marked with [Flags] so we can
        //    combine several preferences with a bitwise OR.
        // ---------------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);
            // Hide window UI, toolbar and menubar – this also removes scrollbars.
            editor.ChangeViewerPreference(
                ViewerPreference.HideWindowUI |
                ViewerPreference.HideToolbar |
                ViewerPreference.HideMenubar);
            editor.Save(outputPath);
        }

        // ---------------------------------------------------------------------
        // 3. Disable page transition effects – set duration to 0 and clear the
        //    transition type.
        // ---------------------------------------------------------------------
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(outputPath);
            pageEditor.TransitionDuration = 0; // no animation
            pageEditor.TransitionType = 0;     // clear any transition type
            pageEditor.ApplyChanges();
            pageEditor.Save(outputPath);
        }
    }
}
