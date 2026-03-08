using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "sample.pdf";
        const string outputPdf = "sample_modified.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the PDF with PdfContentEditor (a Facades class) and read
        //    the current viewer preferences.
        // -----------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);

            // Get the current viewer preference flags (bitmask).
            int currentPrefs = editor.GetViewerPreference();

            Console.WriteLine("Current Viewer Preference Flags:");
            PrintViewerPreferenceFlags(currentPrefs);

            // -----------------------------------------------------------------
            // 2. Modify the preferences:
            //    - Hide the menu bar (ViewerPreference.HideMenubar)
            //    - Use a single page layout (ViewerPreference.PageLayoutSinglePage)
            //    - Fit the window to the first page (ViewerPreference.FitWindow)
            // -----------------------------------------------------------------
            int newPrefs = currentPrefs;
            newPrefs |= ViewerPreference.HideMenubar;               // hide menubar
            newPrefs &= ~ViewerPreference.PageLayoutTwoColumnLeft; // ensure two‑column flag cleared
            newPrefs &= ~ViewerPreference.PageLayoutTwoColumnRight;
            newPrefs &= ~ViewerPreference.PageLayoutOneColumn;
            newPrefs |= ViewerPreference.PageLayoutSinglePage;     // single page layout
            newPrefs |= ViewerPreference.FitWindow;                // fit window to first page

            // Apply the new viewer preferences.
            editor.ChangeViewerPreference(newPrefs);

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");

        // -----------------------------------------------------------------
        // 3. (Optional) Demonstrate printing the PDF with PdfViewer using
        //    some of the viewer‑related properties.
        // -----------------------------------------------------------------
        using (PdfViewer viewer = new PdfViewer())
        {
            viewer.BindPdf(outputPdf);
            viewer.AutoResize = true;          // scale to printable area
            viewer.AutoRotate = true;          // rotate pages automatically
            viewer.PrintPageDialog = false;    // suppress page‑number dialog
            // Note: In a real environment you would call viewer.PrintDocument()
            //       to send the job to a printer. Here we just illustrate the API.
            Console.WriteLine("PdfViewer configured (AutoResize, AutoRotate, PrintPageDialog).");
            viewer.Close();
        }
    }

    // Helper method to decode and display individual ViewerPreference flags.
    static void PrintViewerPreferenceFlags(int flags)
    {
        void ShowFlag(int flag, string name) =>
            Console.WriteLine($"{name}: {(flags & flag) != 0}");

        ShowFlag(ViewerPreference.CenterWindow,               "CenterWindow");
        ShowFlag(ViewerPreference.DirectionL2R,               "DirectionL2R");
        ShowFlag(ViewerPreference.DirectionR2L,               "DirectionR2L");
        ShowFlag(ViewerPreference.DisplayDocTitle,            "DisplayDocTitle");
        ShowFlag(ViewerPreference.DuplexFlipLongEdge,        "DuplexFlipLongEdge");
        ShowFlag(ViewerPreference.DuplexFlipShortEdge,       "DuplexFlipShortEdge");
        ShowFlag(ViewerPreference.FitWindow,                 "FitWindow");
        ShowFlag(ViewerPreference.HideMenubar,                "HideMenubar");
        ShowFlag(ViewerPreference.HideToolbar,                "HideToolbar");
        ShowFlag(ViewerPreference.HideWindowUI,               "HideWindowUI");
        ShowFlag(ViewerPreference.NonFullScreenPageModeUseNone, "NonFullScreenPageModeUseNone");
        ShowFlag(ViewerPreference.NonFullScreenPageModeUseOC,   "NonFullScreenPageModeUseOC");
        ShowFlag(ViewerPreference.NonFullScreenPageModeUseOutlines, "NonFullScreenPageModeUseOutlines");
        ShowFlag(ViewerPreference.NonFullScreenPageModeUseThumbs,   "NonFullScreenPageModeUseThumbs");
        ShowFlag(ViewerPreference.PageLayoutOneColumn,       "PageLayoutOneColumn");
        ShowFlag(ViewerPreference.PageLayoutSinglePage,      "PageLayoutSinglePage");
        ShowFlag(ViewerPreference.PageLayoutTwoColumnLeft,   "PageLayoutTwoColumnLeft");
        ShowFlag(ViewerPreference.PageLayoutTwoColumnRight,  "PageLayoutTwoColumnRight");
        ShowFlag(ViewerPreference.PageModeFullScreen,        "PageModeFullScreen");
        ShowFlag(ViewerPreference.PageModeUseAttachment,     "PageModeUseAttachment");
        ShowFlag(ViewerPreference.PageModeUseNone,           "PageModeUseNone");
        ShowFlag(ViewerPreference.PageModeUseOC,             "PageModeUseOC");
        ShowFlag(ViewerPreference.PageModeUseOutlines,       "PageModeUseOutlines");
        ShowFlag(ViewerPreference.PageModeUseThumbs,         "PageModeUseThumbs");
        ShowFlag(ViewerPreference.PickTrayByPDFSize,        "PickTrayByPDFSize");
        ShowFlag(ViewerPreference.PrintScalingAppDefault,   "PrintScalingAppDefault");
        ShowFlag(ViewerPreference.PrintScalingNone,          "PrintScalingNone");
        ShowFlag(ViewerPreference.Simplex,                   "Simplex");
    }
}