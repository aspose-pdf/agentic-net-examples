using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);
        int combinedPreferences = ViewerPreference.CenterWindow | ViewerPreference.HideToolbar;
        editor.ChangeViewerPreference(combinedPreferences);
        editor.Save(outputPath);
        Console.WriteLine($"PDF saved with combined viewer preferences to '{outputPath}'.");
    }
}