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

        // Create the content editor facade
        PdfContentEditor editor = new PdfContentEditor();

        // Load the PDF document for editing
        editor.BindPdf(inputPath);

        // 1. Hide the menu bar
        editor.ChangeViewerPreference(ViewerPreference.HideMenubar);
        int prefAfterHideMenubar = editor.GetViewerPreference();
        Console.WriteLine($"ViewerPreference after HideMenubar: 0x{prefAfterHideMenubar:X}");

        // 2. Set page mode to "Use None"
        editor.ChangeViewerPreference(ViewerPreference.PageModeUseNone);
        int prefAfterPageMode = editor.GetViewerPreference();
        Console.WriteLine($"ViewerPreference after PageModeUseNone: 0x{prefAfterPageMode:X}");

        // 3. Fit the window to the first page
        editor.ChangeViewerPreference(ViewerPreference.FitWindow);
        int prefAfterFitWindow = editor.GetViewerPreference();
        Console.WriteLine($"ViewerPreference after FitWindow: 0x{prefAfterFitWindow:X}");

        // Save the modified PDF
        editor.Save(outputPath);

        // Release resources
        editor.Close();

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}