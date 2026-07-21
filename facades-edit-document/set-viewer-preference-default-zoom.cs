using System;
using System.IO;
using Aspose.Pdf;
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

        // Step 1: Set viewer preference to continuous layout (one column)
        using (PdfContentEditor viewerEditor = new PdfContentEditor())
        {
            viewerEditor.BindPdf(inputPath);
            // ViewerPreference.PageLayoutOneColumn displays pages in a single column (continuous)
            viewerEditor.ChangeViewerPreference(ViewerPreference.PageLayoutOneColumn);
            viewerEditor.Save(tempPath);
        }

        // Step 2: Ensure default zoom is 100% (Zoom = 1.0)
        using (PdfPageEditor zoomEditor = new PdfPageEditor())
        {
            zoomEditor.BindPdf(tempPath);
            zoomEditor.Zoom = 1.0f; // 100% zoom
            zoomEditor.Save(outputPath);
        }

        // Clean up temporary file
        try { File.Delete(tempPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Viewer preference and zoom applied. Saved to '{outputPath}'.");
    }
}