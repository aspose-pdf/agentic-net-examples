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
        // Hide scrollbars (HideWindowUI) and toolbars (HideToolbar) for a clean view
        int combinedPreference = ViewerPreference.HideWindowUI | ViewerPreference.HideToolbar;
        editor.ChangeViewerPreference(combinedPreference);
        editor.Save(outputPath);
        Console.WriteLine($"Viewer preferences applied and saved to '{outputPath}'.");
    }
}