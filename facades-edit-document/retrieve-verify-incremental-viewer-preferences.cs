using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // ViewerPreference constants

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

        // Bind the PDF and apply viewer preference changes incrementally
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // 1. Hide the menu bar
            editor.ChangeViewerPreference(ViewerPreference.HideMenubar);
            int prefAfterHideMenubar = editor.GetViewerPreference();
            Console.WriteLine($"After HideMenubar: 0x{prefAfterHideMenubar:X}");

            // 2. Show document outline (page mode)
            editor.ChangeViewerPreference(ViewerPreference.PageModeUseOutlines);
            int prefAfterOutline = editor.GetViewerPreference();
            Console.WriteLine($"After PageModeUseOutlines: 0x{prefAfterOutline:X}");

            // 3. Fit window to first page
            editor.ChangeViewerPreference(ViewerPreference.FitWindow);
            int prefAfterFitWindow = editor.GetViewerPreference();
            Console.WriteLine($"After FitWindow: 0x{prefAfterFitWindow:X}");

            // Save the modified PDF
            editor.Save(outputPath);
        }

        // Verify the combined preferences in the saved file
        using (PdfContentEditor verifier = new PdfContentEditor())
        {
            verifier.BindPdf(outputPath);
            int finalPref = verifier.GetViewerPreference();
            Console.WriteLine($"Final combined preferences in saved file: 0x{finalPref:X}");
        }
    }
}