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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the content editor facade
            PdfContentEditor editor = new PdfContentEditor();

            // Bind the PDF file to the editor
            editor.BindPdf(inputPath);

            // Change 1: hide the menu bar
            editor.ChangeViewerPreference(ViewerPreference.HideMenubar);
            int prefAfterHideMenubar = editor.GetViewerPreference();
            Console.WriteLine($"ViewerPreference after HideMenubar: 0x{prefAfterHideMenubar:X}");

            // Change 2: fit window to first page
            editor.ChangeViewerPreference(ViewerPreference.FitWindow);
            int prefAfterFitWindow = editor.GetViewerPreference();
            Console.WriteLine($"ViewerPreference after FitWindow: 0x{prefAfterFitWindow:X}");

            // Change 3: set page mode to use outlines (correct enum name)
            editor.ChangeViewerPreference(ViewerPreference.PageModeUseOutlines);
            int prefAfterPageModeOutline = editor.GetViewerPreference();
            Console.WriteLine($"ViewerPreference after PageModeUseOutlines: 0x{prefAfterPageModeOutline:X}");

            // Save the modified PDF
            editor.Save(outputPath);
            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");

            // Release resources held by the facade
            editor.Close();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
