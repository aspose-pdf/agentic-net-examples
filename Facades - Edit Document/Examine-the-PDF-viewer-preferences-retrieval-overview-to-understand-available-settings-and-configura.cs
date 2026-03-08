using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "example.pdf";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the PdfContentEditor facade and bind the PDF.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Retrieve the viewer preference flags as an integer.
        int prefValue = editor.GetViewerPreference();
        Console.WriteLine($"Viewer Preference value: {prefValue}");

        // Helper local function to test a flag and print its name.
        void CheckFlag(int flag, string name)
        {
            if ((prefValue & flag) != 0)
                Console.WriteLine($"- {name} is set");
        }

        // Check a selection of common viewer preferences.
        CheckFlag(ViewerPreference.CenterWindow, "CenterWindow");
        CheckFlag(ViewerPreference.DirectionL2R, "DirectionL2R");
        CheckFlag(ViewerPreference.DirectionR2L, "DirectionR2L");
        CheckFlag(ViewerPreference.DisplayDocTitle, "DisplayDocTitle");
        CheckFlag(ViewerPreference.FitWindow, "FitWindow");
        CheckFlag(ViewerPreference.HideMenubar, "HideMenubar");
        CheckFlag(ViewerPreference.HideToolbar, "HideToolbar");
        CheckFlag(ViewerPreference.HideWindowUI, "HideWindowUI");
        // Corrected enum member name (plural "Outlines")
        CheckFlag(ViewerPreference.PageModeUseOutlines, "PageModeUseOutlines");
        CheckFlag(ViewerPreference.PageModeUseThumbs, "PageModeUseThumbs");
        CheckFlag(ViewerPreference.PageLayoutOneColumn, "PageLayoutOneColumn");
        CheckFlag(ViewerPreference.PageLayoutTwoColumnLeft, "PageLayoutTwoColumnLeft");
        CheckFlag(ViewerPreference.PageLayoutTwoColumnRight, "PageLayoutTwoColumnRight");
        CheckFlag(ViewerPreference.PageLayoutSinglePage, "PageLayoutSinglePage");
        CheckFlag(ViewerPreference.PrintScalingNone, "PrintScalingNone");
        CheckFlag(ViewerPreference.PrintScalingAppDefault, "PrintScalingAppDefault");
        CheckFlag(ViewerPreference.Simplex, "Simplex");
        CheckFlag(ViewerPreference.DuplexFlipLongEdge, "DuplexFlipLongEdge");
        CheckFlag(ViewerPreference.DuplexFlipShortEdge, "DuplexFlipShortEdge");
        // Add more checks as needed.

        // No explicit save is required because we only read preferences.
        // PdfContentEditor does not implement IDisposable, so no using block is needed.
    }
}
