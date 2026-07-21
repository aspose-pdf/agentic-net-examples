using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Mapping of ViewerPreference flag values to friendly names
    static readonly Dictionary<int, string> PreferenceNames = new Dictionary<int, string>
    {
        { ViewerPreference.CenterWindow,               "Center Window" },
        { ViewerPreference.DirectionL2R,               "Reading Direction Left‑to‑Right" },
        { ViewerPreference.DirectionR2L,               "Reading Direction Right‑to‑Left" },
        { ViewerPreference.DisplayDocTitle,            "Display Document Title" },
        { ViewerPreference.DuplexFlipLongEdge,        "Duplex Flip Long Edge" },
        { ViewerPreference.DuplexFlipShortEdge,       "Duplex Flip Short Edge" },
        { ViewerPreference.FitWindow,                 "Fit Window to First Page" },
        { ViewerPreference.HideMenubar,                "Hide Menu Bar" },
        { ViewerPreference.HideToolbar,                "Hide Toolbar" },
        { ViewerPreference.HideWindowUI,               "Hide Window UI" },
        { ViewerPreference.NonFullScreenPageModeUseNone,   "Non‑Full‑Screen: No Outline/Thumbs" },
        { ViewerPreference.NonFullScreenPageModeUseOC,     "Non‑Full‑Screen: Optional Content Panel" },
        { ViewerPreference.NonFullScreenPageModeUseOutlines,"Non‑Full‑Screen: Outline Visible" },
        { ViewerPreference.NonFullScreenPageModeUseThumbs, "Non‑Full‑Screen: Thumbnails Visible" },
        { ViewerPreference.PageLayoutOneColumn,       "Page Layout: One Column" },
        { ViewerPreference.PageLayoutSinglePage,      "Page Layout: Single Page" },
        { ViewerPreference.PageLayoutTwoColumnLeft,   "Page Layout: Two Columns (Left)" },
        { ViewerPreference.PageLayoutTwoColumnRight,  "Page Layout: Two Columns (Right)" },
        { ViewerPreference.PageModeFullScreen,        "Page Mode: Full Screen" },
        { ViewerPreference.PageModeUseAttachment,     "Page Mode: Attachments Panel" },
        { ViewerPreference.PageModeUseNone,           "Page Mode: No Outline/Thumbs" },
        { ViewerPreference.PageModeUseOC,             "Page Mode: Optional Content Panel" },
        { ViewerPreference.PageModeUseOutlines,       "Page Mode: Outline Visible" },
        { ViewerPreference.PageModeUseThumbs,         "Page Mode: Thumbnails Visible" },
        { ViewerPreference.PickTrayByPDFSize,         "Pick Tray By PDF Size" },
        { ViewerPreference.PrintScalingAppDefault,    "Print Scaling: Application Default" },
        { ViewerPreference.PrintScalingNone,          "Print Scaling: None" },
        { ViewerPreference.Simplex,                   "Simplex (Single‑Sided Printing)" }
    };

    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_customized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF into PdfContentEditor
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Retrieve current viewer preferences as a bitmask
            int currentPrefs = editor.GetViewerPreference();

            // Display current preferences as toggle states
            Console.WriteLine("Current Viewer Preferences:");
            foreach (var kvp in PreferenceNames)
            {
                bool isSet = (currentPrefs & kvp.Key) != 0;
                Console.WriteLine($"  [{(isSet ? "X" : " ")}] {kvp.Value}");
            }

            // Example: toggle a few preferences based on user input (simulated here)
            // In a real UI, these booleans would come from checkboxes, etc.
            bool hideMenubar   = true;   // user wants to hide the menu bar
            bool fitWindow     = false;  // user wants to disable fit‑window
            bool pageModeThumb = true;   // user wants thumbnail panel in page mode

            // Build a new preference mask
            int newPrefs = currentPrefs;

            // Apply HideMenubar toggle
            if (hideMenubar)
                newPrefs |= ViewerPreference.HideMenubar;
            else
                newPrefs &= ~ViewerPreference.HideMenubar;

            // Apply FitWindow toggle
            if (fitWindow)
                newPrefs |= ViewerPreference.FitWindow;
            else
                newPrefs &= ~ViewerPreference.FitWindow;

            // Apply PageModeUseThumbs toggle
            if (pageModeThumb)
                newPrefs |= ViewerPreference.PageModeUseThumbs;
            else
                newPrefs &= ~ViewerPreference.PageModeUseThumbs;

            // If any changes were made, update the document
            if (newPrefs != currentPrefs)
            {
                editor.ChangeViewerPreference(newPrefs);
                editor.Save(outputPath);
                Console.WriteLine($"Updated preferences saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("No preference changes detected; document not saved.");
            }
        }
    }
}