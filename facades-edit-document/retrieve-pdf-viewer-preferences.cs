using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);
        int prefValue = editor.GetViewerPreference();

        Console.WriteLine($"Viewer Preference flags: {prefValue}");

        // Example checks for a few common flags
        if ((prefValue & ViewerPreference.HideMenubar) != 0)
        {
            Console.WriteLine("HideMenubar flag is set.");
        }
        if ((prefValue & ViewerPreference.PageModeUseOutlines) != 0)
        {
            Console.WriteLine("PageModeUseOutlines flag is set.");
        }
        if ((prefValue & ViewerPreference.FitWindow) != 0)
        {
            Console.WriteLine("FitWindow flag is set.");
        }
    }
}