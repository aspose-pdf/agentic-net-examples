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

        // Create the editor and bind the existing PDF (load step)
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Combine desired viewer preferences using bitwise OR
        int viewerPrefs = ViewerPreference.HideMenubar |
                          ViewerPreference.HideToolbar |
                          ViewerPreference.FitWindow |
                          ViewerPreference.PageModeUseNone |
                          ViewerPreference.PageLayoutSinglePage;

        // Apply the preferences (change step)
        editor.ChangeViewerPreference(viewerPrefs);

        // Save the modified PDF (save step)
        editor.Save(outputPath);

        Console.WriteLine($"Viewer preferences applied and saved to '{outputPath}'.");
    }
}