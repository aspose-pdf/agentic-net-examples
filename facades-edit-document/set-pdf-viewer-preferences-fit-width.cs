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

        // ------------------------------------------------------------
        // 1. Set viewer preferences:
        //    - Show thumbnail pane (NonFullScreenPageModeUseThumbs)
        //    - Resize window to fit the first page (FitWindow)
        // ------------------------------------------------------------
        PdfContentEditor viewerEditor = new PdfContentEditor();
        viewerEditor.BindPdf(inputPath);

        // Combine the two flags using bitwise OR
        int viewerPrefs = ViewerPreference.NonFullScreenPageModeUseThumbs |
                          ViewerPreference.FitWindow;

        viewerEditor.ChangeViewerPreference(viewerPrefs);
        viewerEditor.Save(outputPath);
        viewerEditor.Close();

        // ------------------------------------------------------------
        // 2. Set default zoom to fit width.
        //    PdfPageEditor.Zoom controls the default zoom factor.
        //    Setting it to 1.0 (100%) lets the viewer display the page
        //    at its natural size, which typically results in a "fit width"
        //    view when the window is resized.
        // ------------------------------------------------------------
        PdfPageEditor zoomEditor = new PdfPageEditor();
        zoomEditor.BindPdf(outputPath);
        zoomEditor.Zoom = 1.0f; // 100% zoom (fit width)
        zoomEditor.Save(outputPath);
        zoomEditor.Close();

        Console.WriteLine($"PDF saved with viewer preferences to '{outputPath}'.");
    }
}