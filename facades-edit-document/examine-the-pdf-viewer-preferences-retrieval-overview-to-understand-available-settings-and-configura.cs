using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path (after any modifications)
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using PdfContentEditor (facade for content editing)
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Retrieve the current viewer‑preference flags as an integer bitmask
            int prefMask = editor.GetViewerPreference();

            // Local helper to test whether a specific flag is set
            bool HasFlag(int flag) => (prefMask & flag) != 0;

            // Display a selection of common viewer‑preference settings
            Console.WriteLine("Current Viewer Preferences:");
            Console.WriteLine($"CenterWindow               : {HasFlag(ViewerPreference.CenterWindow)}");
            Console.WriteLine($"FitWindow                  : {HasFlag(ViewerPreference.FitWindow)}");
            Console.WriteLine($"HideMenubar                : {HasFlag(ViewerPreference.HideMenubar)}");
            Console.WriteLine($"HideToolbar                : {HasFlag(ViewerPreference.HideToolbar)}");
            Console.WriteLine($"HideWindowUI               : {HasFlag(ViewerPreference.HideWindowUI)}");
            Console.WriteLine($"DisplayDocTitle            : {HasFlag(ViewerPreference.DisplayDocTitle)}");
            Console.WriteLine($"PageModeUseNone            : {HasFlag(ViewerPreference.PageModeUseNone)}");
            Console.WriteLine($"PageModeUseOutlines        : {HasFlag(ViewerPreference.PageModeUseOutlines)}");
            Console.WriteLine($"PageLayoutSinglePage       : {HasFlag(ViewerPreference.PageLayoutSinglePage)}");
            Console.WriteLine($"NonFullScreenPageModeThumbs: {HasFlag(ViewerPreference.NonFullScreenPageModeUseThumbs)}");

            // Example modification: ensure the menubar is hidden
            // ChangeViewerPreference adds the specified flag; calling it multiple times adds more flags.
            editor.ChangeViewerPreference(ViewerPreference.HideMenubar);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}