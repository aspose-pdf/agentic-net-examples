using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes for PDF manipulation

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_viewer_prefs.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor is a facade for editing PDF viewer preferences.
        // It does NOT implement IDisposable, so no using block is required.
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the source PDF.
        editor.BindPdf(inputPath);

        // Combine viewer preference flags:
        //   NonFullScreenPageModeUseThumbs – show thumbnail pane.
        //   FitWindow – resize the window to fit the first displayed page
        //               (commonly used to achieve a “fit‑width” effect).
        int viewerPrefs = ViewerPreference.NonFullScreenPageModeUseThumbs |
                          ViewerPreference.FitWindow;

        // Apply the combined preferences.
        editor.ChangeViewerPreference(viewerPrefs);

        // Save the modified PDF.
        editor.Save(outputPath);

        Console.WriteLine($"PDF saved with viewer preferences: {outputPath}");
    }
}