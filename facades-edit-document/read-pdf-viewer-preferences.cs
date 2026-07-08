using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the content editor
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Retrieve the viewer preference flags as an integer
        int pref = editor.GetViewerPreference();
        Console.WriteLine($"Viewer Preference value: {pref}");

        // Helper to display each flag that is set
        void CheckFlag(int flag, string name)
        {
            if ((pref & flag) != 0)
                Console.WriteLine($"- {name}");
        }

        // List of possible flags (constants are defined in ViewerPreference)
        CheckFlag(ViewerPreference.CenterWindow, "CenterWindow");
        CheckFlag(ViewerPreference.DirectionL2R, "DirectionL2R");
        CheckFlag(ViewerPreference.DirectionR2L, "DirectionR2L");
        CheckFlag(ViewerPreference.DisplayDocTitle, "DisplayDocTitle");
        CheckFlag(ViewerPreference.DuplexFlipLongEdge, "DuplexFlipLongEdge");
        CheckFlag(ViewerPreference.DuplexFlipShortEdge, "DuplexFlipShortEdge");
        CheckFlag(ViewerPreference.FitWindow, "FitWindow");
        CheckFlag(ViewerPreference.HideMenubar, "HideMenubar");
        CheckFlag(ViewerPreference.HideToolbar, "HideToolbar");
        CheckFlag(ViewerPreference.HideWindowUI, "HideWindowUI");
        CheckFlag(ViewerPreference.NonFullScreenPageModeUseNone, "NonFullScreenPageModeUseNone");
        CheckFlag(ViewerPreference.NonFullScreenPageModeUseOC, "NonFullScreenPageModeUseOC");
        CheckFlag(ViewerPreference.NonFullScreenPageModeUseOutlines, "NonFullScreenPageModeUseOutlines");
        CheckFlag(ViewerPreference.NonFullScreenPageModeUseThumbs, "NonFullScreenPageModeUseThumbs");
        CheckFlag(ViewerPreference.PageLayoutOneColumn, "PageLayoutOneColumn");
        CheckFlag(ViewerPreference.PageLayoutSinglePage, "PageLayoutSinglePage");
        CheckFlag(ViewerPreference.PageLayoutTwoColumnLeft, "PageLayoutTwoColumnLeft");
        CheckFlag(ViewerPreference.PageLayoutTwoColumnRight, "PageLayoutTwoColumnRight");
        CheckFlag(ViewerPreference.PageModeFullScreen, "PageModeFullScreen");
        CheckFlag(ViewerPreference.PageModeUseAttachment, "PageModeUseAttachment");
        CheckFlag(ViewerPreference.PageModeUseNone, "PageModeUseNone");
        CheckFlag(ViewerPreference.PageModeUseOC, "PageModeUseOC");
        CheckFlag(ViewerPreference.PageModeUseOutlines, "PageModeUseOutlines");
        CheckFlag(ViewerPreference.PageModeUseThumbs, "PageModeUseThumbs");
        CheckFlag(ViewerPreference.PickTrayByPDFSize, "PickTrayByPDFSize");
        CheckFlag(ViewerPreference.PrintScalingAppDefault, "PrintScalingAppDefault");
        CheckFlag(ViewerPreference.PrintScalingNone, "PrintScalingNone");
        CheckFlag(ViewerPreference.Simplex, "Simplex");
    }
}