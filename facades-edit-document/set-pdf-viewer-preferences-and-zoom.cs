using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_viewer_pref.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // ---------- Set viewer preferences ----------
            // Show thumbnail pane (NonFullScreenPageModeUseThumbs) and resize window to fit first page (FitWindow)
            PdfContentEditor viewerPrefEditor = new PdfContentEditor();
            viewerPrefEditor.BindPdf(inputPath);
            int pref = ViewerPreference.NonFullScreenPageModeUseThumbs | ViewerPreference.FitWindow;
            viewerPrefEditor.ChangeViewerPreference(pref);
            viewerPrefEditor.Save(outputPath);
            viewerPrefEditor.Close();

            // ---------- Set default zoom (fit width) ----------
            // Using PdfPageEditor to set a zoom factor; 1.0 = 100% (default). Adjust as needed for fit‑width.
            PdfPageEditor zoomEditor = new PdfPageEditor();
            zoomEditor.BindPdf(outputPath);
            zoomEditor.Zoom = 1.0f; // 100% zoom; modify if a different default is required.
            zoomEditor.Save(outputPath);
            zoomEditor.Close();

            Console.WriteLine($"PDF saved with viewer preferences to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}