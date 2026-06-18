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

        // Bind the PDF to the PdfContentEditor facade
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Retrieve the viewer preference flags as an integer
        int pref = editor.GetViewerPreference();

        Console.WriteLine($"Viewer Preference value: {pref}");

        // Helper to print each flag that is set
        void PrintFlag(string name, int flag)
        {
            if ((pref & flag) != 0)
                Console.WriteLine($"- {name}");
        }

        // List of all ViewerPreference flags
        PrintFlag("CenterWindow", ViewerPreference.CenterWindow);
        PrintFlag("DirectionL2R", ViewerPreference.DirectionL2R);
        PrintFlag("DirectionR2L", ViewerPreference.DirectionR2L);
        PrintFlag("DisplayDocTitle", ViewerPreference.DisplayDocTitle);
        PrintFlag("DuplexFlipLongEdge", ViewerPreference.DuplexFlipLongEdge);
        PrintFlag("DuplexFlipShortEdge", ViewerPreference.DuplexFlipShortEdge);
        PrintFlag("FitWindow", ViewerPreference.FitWindow);
        PrintFlag("HideMenubar", ViewerPreference.HideMenubar);
        PrintFlag("HideToolbar", ViewerPreference.HideToolbar);
        PrintFlag("HideWindowUI", ViewerPreference.HideWindowUI);
        PrintFlag("NonFullScreenPageModeUseNone", ViewerPreference.NonFullScreenPageModeUseNone);
        PrintFlag("NonFullScreenPageModeUseOC", ViewerPreference.NonFullScreenPageModeUseOC);
        PrintFlag("NonFullScreenPageModeUseOutlines", ViewerPreference.NonFullScreenPageModeUseOutlines);
        PrintFlag("NonFullScreenPageModeUseThumbs", ViewerPreference.NonFullScreenPageModeUseThumbs);
        PrintFlag("PageLayoutOneColumn", ViewerPreference.PageLayoutOneColumn);
        PrintFlag("PageLayoutSinglePage", ViewerPreference.PageLayoutSinglePage);
        PrintFlag("PageLayoutTwoColumnLeft", ViewerPreference.PageLayoutTwoColumnLeft);
        PrintFlag("PageLayoutTwoColumnRight", ViewerPreference.PageLayoutTwoColumnRight);
        PrintFlag("PageModeFullScreen", ViewerPreference.PageModeFullScreen);
        PrintFlag("PageModeUseAttachment", ViewerPreference.PageModeUseAttachment);
        PrintFlag("PageModeUseNone", ViewerPreference.PageModeUseNone);
        PrintFlag("PageModeUseOC", ViewerPreference.PageModeUseOC);
        PrintFlag("PageModeUseOutlines", ViewerPreference.PageModeUseOutlines);
        PrintFlag("PageModeUseThumbs", ViewerPreference.PageModeUseThumbs);
        PrintFlag("PickTrayByPDFSize", ViewerPreference.PickTrayByPDFSize);
        PrintFlag("PrintScalingAppDefault", ViewerPreference.PrintScalingAppDefault);
        PrintFlag("PrintScalingNone", ViewerPreference.PrintScalingNone);
        PrintFlag("Simplex", ViewerPreference.Simplex);
    }
}