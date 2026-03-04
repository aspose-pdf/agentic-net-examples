using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfContentEditor, ViewerPreference

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
            // Create the editor and bind the source PDF
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(inputPath);

            // Set desired viewer preferences using flags from ViewerPreference
            // Example: hide the menu bar and use no page mode
            editor.ChangeViewerPreference(ViewerPreference.HideMenubar);
            editor.ChangeViewerPreference(ViewerPreference.PageModeUseNone);

            // Save the modified PDF
            editor.Save(outputPath);

            Console.WriteLine($"Viewer preferences applied and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}