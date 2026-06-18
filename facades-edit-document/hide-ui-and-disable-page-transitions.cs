using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_clean.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // First, hide UI elements (scrollbars, toolbars, menu bar) using PdfContentEditor.
        using (PdfContentEditor viewerEditor = new PdfContentEditor())
        {
            viewerEditor.BindPdf(inputPath);
            viewerEditor.ChangeViewerPreference(ViewerPreference.HideWindowUI); // hide scrollbars and navigation UI
            viewerEditor.ChangeViewerPreference(ViewerPreference.HideToolbar); // optional: hide toolbars
            viewerEditor.ChangeViewerPreference(ViewerPreference.HideMenubar); // optional: hide menu bar

            // Save the intermediate result to a temporary file.
            string tempPath = Path.GetTempFileName();
            viewerEditor.Save(tempPath);

            // Then, disable any page transition effects using PdfPageEditor.
            using (PdfPageEditor pageEditor = new PdfPageEditor())
            {
                pageEditor.BindPdf(tempPath);
                // Apply to all pages (default behavior).
                pageEditor.TransitionType = 0;      // 0 = no transition
                pageEditor.TransitionDuration = 0; // duration 0 seconds
                pageEditor.ApplyChanges();
                pageEditor.Save(outputPath);
            }

            // Clean up the temporary file.
            File.Delete(tempPath);
        }

        Console.WriteLine($"Clean PDF saved to '{outputPath}'.");
    }
}