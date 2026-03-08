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

        // Bind the PDF, set viewer preferences, and save.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Combine desired viewer preference flags.
            int preferences =
                ViewerPreference.HideMenubar |
                ViewerPreference.HideToolbar |
                ViewerPreference.CenterWindow |
                ViewerPreference.FitWindow |
                ViewerPreference.PageLayoutOneColumn |
                ViewerPreference.PageModeUseOutlines |
                ViewerPreference.DisplayDocTitle |
                ViewerPreference.PrintScalingNone |
                ViewerPreference.Simplex;

            editor.ChangeViewerPreference(preferences);
            editor.Save(outputPath);
        }

        Console.WriteLine($"Viewer preferences applied and saved to '{outputPath}'.");
    }
}