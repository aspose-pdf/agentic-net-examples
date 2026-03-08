using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "example.pdf";
        const string outputPath = "example_out.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the content editor and bind the source PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Apply viewer preferences: hide the menu bar and use no page mode
        editor.ChangeViewerPreference(ViewerPreference.HideMenubar);
        editor.ChangeViewerPreference(ViewerPreference.PageModeUseNone);

        // Save the modified PDF
        editor.Save(outputPath);

        Console.WriteLine($"Viewer preferences updated and saved to '{outputPath}'.");
    }
}