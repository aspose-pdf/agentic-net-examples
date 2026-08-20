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

        // Bind the PDF to the content editor facade
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Retrieve the viewer preference flags as an integer
            int pref = editor.GetViewerPreference();
            Console.WriteLine($"Viewer Preference value: {pref}");

            // Helper to print each flag that is set
            void PrintFlag(int flag, string name)
            {
                if ((pref & flag) != 0)
                    Console.WriteLine($"- {name}");
            }

            // Enumerate all known ViewerPreference flags
            PrintFlag(ViewerPreference.CenterWindow, "CenterWindow");
            PrintFlag(ViewerPreference.DirectionL2R, "DirectionL2R");
            PrintFlag(ViewerPreference.DirectionR2L, "DirectionR2L");
            PrintFlag(ViewerPreference.DisplayDocTitle, "DisplayDocTitle");
            PrintFlag(ViewerPreference.DuplexFlipLongEdge, "DuplexFlipLongEdge");
            PrintFlag(ViewerPreference.DuplexFlipShortEdge, "DuplexFlipShortEdge");
            PrintFlag(ViewerPreference.FitWindow, "FitWindow");
            PrintFlag(ViewerPreference.HideMenubar, "HideMenubar");
            PrintFlag(ViewerPreference.HideToolbar, "HideToolbar");
            PrintFlag(ViewerPreference.HideWindowUI, "HideWindowUI");
            PrintFlag(ViewerPreference.NonFullScreenPageModeUseNone, "NonFullScreenPageModeUseNone");
            PrintFlag(ViewerPreference.NonFullScreenPageModeUseOC, "NonFullScreenPageModeUseOC");
            PrintFlag(ViewerPreference.NonFullScreenPageModeUseOutlines, "NonFullScreenPageModeUseOutlines");
            PrintFlag(ViewerPreference.NonFullScreenPageModeUseThumbs, "NonFullScreenPageModeUseThumbs");
            PrintFlag(ViewerPreference.PageLayoutOneColumn, "PageLayoutOneColumn");
            PrintFlag(ViewerPreference.PageLayoutSinglePage, "PageLayoutSinglePage");
            PrintFlag(ViewerPreference.PageLayoutTwoColumnLeft, "PageLayoutTwoColumnLeft");
            PrintFlag(ViewerPreference.PageLayoutTwoColumnRight, "PageLayoutTwoColumnRight");
            PrintFlag(ViewerPreference.PageModeFullScreen, "PageModeFullScreen");
            PrintFlag(ViewerPreference.PageModeUseAttachment, "PageModeUseAttachment");
            PrintFlag(ViewerPreference.PageModeUseNone, "PageModeUseNone");
            PrintFlag(ViewerPreference.PageModeUseOC, "PageModeUseOC");
            PrintFlag(ViewerPreference.PageModeUseOutlines, "PageModeUseOutlines");
            PrintFlag(ViewerPreference.PageModeUseThumbs, "PageModeUseThumbs");
            PrintFlag(ViewerPreference.PickTrayByPDFSize, "PickTrayByPDFSize");
            PrintFlag(ViewerPreference.PrintScalingAppDefault, "PrintScalingAppDefault");
            PrintFlag(ViewerPreference.PrintScalingNone, "PrintScalingNone");
            PrintFlag(ViewerPreference.Simplex, "Simplex");
        }
    }
}