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

        // Initialize the facade and bind the PDF file
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Retrieve the viewer preference flags as an integer (Aspose returns int)
        int viewerPref = editor.GetViewerPreference();

        // Example: interpret some common flags using bitwise checks
        if ((viewerPref & (int)ViewerPreference.HideMenubar) != 0)
            Console.WriteLine("Viewer Preference: HideMenubar is enabled.");

        if ((viewerPref & (int)ViewerPreference.NonFullScreenPageModeUseOutlines) != 0)
            Console.WriteLine("Viewer Preference: NonFullScreenPageModeUseOutlines is enabled.");

        if ((viewerPref & (int)ViewerPreference.FitWindow) != 0)
            Console.WriteLine("Viewer Preference: FitWindow is enabled.");

        // Additional flags can be checked similarly using ViewerPreference constants
    }
}
