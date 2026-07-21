using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfContentEditor, ViewerPreference

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

        try
        {
            // Bind the PDF and apply viewer preference changes incrementally
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(inputPath);

                // Change 1: hide the menu bar
                editor.ChangeViewerPreference(ViewerPreference.HideMenubar);
                int prefAfterHideMenubar = editor.GetViewerPreference();
                Console.WriteLine($"After HideMenubar: 0x{prefAfterHideMenubar:X}");

                // Change 2: set page mode to none
                editor.ChangeViewerPreference(ViewerPreference.PageModeUseNone);
                int prefAfterPageModeNone = editor.GetViewerPreference();
                Console.WriteLine($"After PageModeUseNone: 0x{prefAfterPageModeNone:X}");

                // Change 3: fit window to first page
                editor.ChangeViewerPreference(ViewerPreference.FitWindow);
                int prefAfterFitWindow = editor.GetViewerPreference();
                Console.WriteLine($"After FitWindow: 0x{prefAfterFitWindow:X}");

                // Save the edited PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}