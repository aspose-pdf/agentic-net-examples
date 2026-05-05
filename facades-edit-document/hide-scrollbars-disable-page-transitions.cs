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

        // Temporary file to hold intermediate changes
        string tempPath = Path.GetTempFileName();

        // ---------- Disable page transition effects ----------
        // PdfPageEditor allows setting transition type and duration.
        // Setting both to zero removes any transition effect.
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(inputPath);
            pageEditor.TransitionDuration = 0; // No transition duration
            pageEditor.TransitionType = 0;     // No transition type (default)
            pageEditor.ApplyChanges();
            pageEditor.Save(tempPath);         // Save intermediate result
        }

        // ---------- Hide UI elements such as scrollbars ----------
        // PdfContentEditor.ChangeViewerPreference can hide the window UI.
        using (PdfContentEditor viewerEditor = new PdfContentEditor())
        {
            viewerEditor.BindPdf(tempPath);
            viewerEditor.ChangeViewerPreference(ViewerPreference.HideWindowUI);
            viewerEditor.Save(outputPath); // Final output
        }

        // Clean up temporary file
        try { File.Delete(tempPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}