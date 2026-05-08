using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class ViewerPreferenceMapper
{
    // Mapping of viewer preference flags to user‑friendly names
    private static readonly Dictionary<int, string> PreferenceMap = new Dictionary<int, string>
    {
        { ViewerPreference.CenterWindow,          "Center Window" },
        { ViewerPreference.DirectionL2R,          "Reading Direction: Left‑to‑Right" },
        { ViewerPreference.DirectionR2L,          "Reading Direction: Right‑to‑Left" },
        { ViewerPreference.DisplayDocTitle,       "Display Document Title" },
        { ViewerPreference.FitWindow,            "Fit Window to First Page" },
        { ViewerPreference.HideMenubar,           "Hide Menu Bar" },
        { ViewerPreference.HideToolbar,           "Hide Toolbar" },
        { ViewerPreference.HideWindowUI,          "Hide Window UI (scrollbars, navigation)" },
        { ViewerPreference.NonFullScreenPageModeUseNone,      "Non‑Full‑Screen: No Outline/Thumbs" },
        { ViewerPreference.NonFullScreenPageModeUseOC,        "Non‑Full‑Screen: Show Optional Content Panel" },
        { ViewerPreference.NonFullScreenPageModeUseOutlines,  "Non‑Full‑Screen: Show Document Outline" },
        { ViewerPreference.NonFullScreenPageModeUseThumbs,    "Non‑Full‑Screen: Show Thumbnails" },
        { ViewerPreference.PageLayoutOneColumn,   "Page Layout: One Column" },
        { ViewerPreference.PageLayoutSinglePage,  "Page Layout: Single Page" },
        { ViewerPreference.PageLayoutTwoColumnLeft,  "Page Layout: Two Columns (Left)" },
        { ViewerPreference.PageLayoutTwoColumnRight, "Page Layout: Two Columns (Right)" },
        { ViewerPreference.PageModeFullScreen,    "Page Mode: Full Screen" },
        { ViewerPreference.PageModeUseAttachment, "Page Mode: Show Attachments" },
        { ViewerPreference.PageModeUseNone,       "Page Mode: None" },
        { ViewerPreference.PageModeUseOC,         "Page Mode: Show Optional Content Panel" },
        { ViewerPreference.PageModeUseOutlines,   "Page Mode: Show Document Outline" },
        { ViewerPreference.PageModeUseThumbs,     "Page Mode: Show Thumbnails" },
        { ViewerPreference.PickTrayByPDFSize,     "Pick Tray By PDF Size" },
        { ViewerPreference.PrintScalingAppDefault,"Print Scaling: Application Default" },
        { ViewerPreference.PrintScalingNone,      "Print Scaling: None" },
        { ViewerPreference.Simplex,               "Simplex (Single‑Sided Printing)" },
        { ViewerPreference.DuplexFlipLongEdge,   "Duplex Flip Long Edge" },
        { ViewerPreference.DuplexFlipShortEdge,  "Duplex Flip Short Edge" }
    };

    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_customized.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF and retrieve current viewer preferences
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);
            int currentPrefs = editor.GetViewerPreference();

            // Interactive toggle loop
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Current Viewer Preference Settings:");
                int index = 1;
                var keys = new List<int>(PreferenceMap.Keys);
                foreach (int flag in keys)
                {
                    bool isSet = (currentPrefs & flag) != 0;
                    Console.WriteLine($"{index,2}. [{(isSet ? "X" : " ")}] {PreferenceMap[flag]}");
                    index++;
                }
                Console.WriteLine(" 0. Save and Exit");
                Console.Write("\nEnter number to toggle (or 0 to finish): ");

                string input = Console.ReadLine();
                if (!int.TryParse(input, out int choice) || choice < 0 || choice > keys.Count)
                {
                    continue; // invalid input, redisplay
                }

                if (choice == 0)
                    break; // exit loop

                int selectedFlag = keys[choice - 1];
                // Toggle the flag
                if ((currentPrefs & selectedFlag) != 0)
                    currentPrefs &= ~selectedFlag; // clear
                else
                    currentPrefs |= selectedFlag;  // set
            }

            // Apply the modified preferences back to the document.
            // Clear existing preferences by resetting the document's flags first.
            // Since Aspose.Pdf does not provide a direct "clear all" method,
            // we re‑apply each flag that is now set.
            foreach (int flag in PreferenceMap.Keys)
            {
                if ((currentPrefs & flag) != 0)
                {
                    editor.ChangeViewerPreference(flag);
                }
            }

            // Save the updated PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Customized PDF saved as '{outputPdf}'.");
    }
}