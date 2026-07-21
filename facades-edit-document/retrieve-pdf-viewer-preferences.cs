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

        // Bind the PDF and retrieve viewer preferences
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);
        int pref = editor.GetViewerPreference();

        // Output the active viewer preference flags
        Console.WriteLine("Viewer Preferences:");
        if ((pref & ViewerPreference.CenterWindow) != 0) Console.WriteLine("- CenterWindow");
        if ((pref & ViewerPreference.DirectionL2R) != 0) Console.WriteLine("- DirectionL2R");
        if ((pref & ViewerPreference.DirectionR2L) != 0) Console.WriteLine("- DirectionR2L");
        if ((pref & ViewerPreference.DisplayDocTitle) != 0) Console.WriteLine("- DisplayDocTitle");
        if ((pref & ViewerPreference.DuplexFlipLongEdge) != 0) Console.WriteLine("- DuplexFlipLongEdge");
        if ((pref & ViewerPreference.DuplexFlipShortEdge) != 0) Console.WriteLine("- DuplexFlipShortEdge");
        if ((pref & ViewerPreference.FitWindow) != 0) Console.WriteLine("- FitWindow");
        if ((pref & ViewerPreference.HideMenubar) != 0) Console.WriteLine("- HideMenubar");
        if ((pref & ViewerPreference.HideToolbar) != 0) Console.WriteLine("- HideToolbar");
        if ((pref & ViewerPreference.HideWindowUI) != 0) Console.WriteLine("- HideWindowUI");
        if ((pref & ViewerPreference.NonFullScreenPageModeUseNone) != 0) Console.WriteLine("- NonFullScreenPageModeUseNone");
        if ((pref & ViewerPreference.NonFullScreenPageModeUseOC) != 0) Console.WriteLine("- NonFullScreenPageModeUseOC");
        if ((pref & ViewerPreference.NonFullScreenPageModeUseOutlines) != 0) Console.WriteLine("- NonFullScreenPageModeUseOutlines");
        if ((pref & ViewerPreference.NonFullScreenPageModeUseThumbs) != 0) Console.WriteLine("- NonFullScreenPageModeUseThumbs");
        if ((pref & ViewerPreference.PageLayoutOneColumn) != 0) Console.WriteLine("- PageLayoutOneColumn");
        if ((pref & ViewerPreference.PageLayoutSinglePage) != 0) Console.WriteLine("- PageLayoutSinglePage");
        if ((pref & ViewerPreference.PageLayoutTwoColumnLeft) != 0) Console.WriteLine("- PageLayoutTwoColumnLeft");
        if ((pref & ViewerPreference.PageLayoutTwoColumnRight) != 0) Console.WriteLine("- PageLayoutTwoColumnRight");
        if ((pref & ViewerPreference.PageModeFullScreen) != 0) Console.WriteLine("- PageModeFullScreen");
        if ((pref & ViewerPreference.PageModeUseAttachment) != 0) Console.WriteLine("- PageModeUseAttachment");
        if ((pref & ViewerPreference.PageModeUseNone) != 0) Console.WriteLine("- PageModeUseNone");
        if ((pref & ViewerPreference.PageModeUseOC) != 0) Console.WriteLine("- PageModeUseOC");
        if ((pref & ViewerPreference.PageModeUseOutlines) != 0) Console.WriteLine("- PageModeUseOutlines");
        if ((pref & ViewerPreference.PageModeUseThumbs) != 0) Console.WriteLine("- PageModeUseThumbs");
        if ((pref & ViewerPreference.PickTrayByPDFSize) != 0) Console.WriteLine("- PickTrayByPDFSize");
        if ((pref & ViewerPreference.PrintScalingAppDefault) != 0) Console.WriteLine("- PrintScalingAppDefault");
        if ((pref & ViewerPreference.PrintScalingNone) != 0) Console.WriteLine("- PrintScalingNone");
        if ((pref & ViewerPreference.Simplex) != 0) Console.WriteLine("- Simplex");

        // Release resources
        editor.Close();
    }
}