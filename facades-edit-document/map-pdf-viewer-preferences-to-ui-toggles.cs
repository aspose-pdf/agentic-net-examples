using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ViewerPreferenceMapper
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the content editor facade and bind the document
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(doc);

                // Retrieve current viewer preferences as a bitmask
                int currentPrefs = editor.GetViewerPreference();

                // Map each viewer preference flag to a UI toggle (simulated here with booleans)
                var toggles = new Dictionary<string, bool>
                {
                    { "HideMenubar",          (currentPrefs & ViewerPreference.HideMenubar) != 0 },
                    { "HideToolbar",          (currentPrefs & ViewerPreference.HideToolbar) != 0 },
                    { "HideWindowUI",         (currentPrefs & ViewerPreference.HideWindowUI) != 0 },
                    { "FitWindow",            (currentPrefs & ViewerPreference.FitWindow) != 0 },
                    { "CenterWindow",         (currentPrefs & ViewerPreference.CenterWindow) != 0 },
                    { "DisplayDocTitle",      (currentPrefs & ViewerPreference.DisplayDocTitle) != 0 },
                    { "PageModeFullScreen",   (currentPrefs & ViewerPreference.PageModeFullScreen) != 0 },
                    { "PageModeUseNone",      (currentPrefs & ViewerPreference.PageModeUseNone) != 0 },
                    { "PageLayoutOneColumn",  (currentPrefs & ViewerPreference.PageLayoutOneColumn) != 0 }
                    // Add other flags as needed
                };

                // Example: toggle some preferences (invert the current state)
                // In a real UI these values would come from user interaction
                toggles["HideMenubar"] = !toggles["HideMenubar"];
                toggles["FitWindow"]   = !toggles["FitWindow"];
                toggles["PageModeFullScreen"] = !toggles["PageModeFullScreen"];

                // Rebuild the preference bitmask based on the toggles
                int newPrefs = 0;
                if (toggles["HideMenubar"])        newPrefs |= ViewerPreference.HideMenubar;
                if (toggles["HideToolbar"])        newPrefs |= ViewerPreference.HideToolbar;
                if (toggles["HideWindowUI"])       newPrefs |= ViewerPreference.HideWindowUI;
                if (toggles["FitWindow"])          newPrefs |= ViewerPreference.FitWindow;
                if (toggles["CenterWindow"])       newPrefs |= ViewerPreference.CenterWindow;
                if (toggles["DisplayDocTitle"])    newPrefs |= ViewerPreference.DisplayDocTitle;
                if (toggles["PageModeFullScreen"]) newPrefs |= ViewerPreference.PageModeFullScreen;
                if (toggles["PageModeUseNone"])    newPrefs |= ViewerPreference.PageModeUseNone;
                if (toggles["PageLayoutOneColumn"])newPrefs |= ViewerPreference.PageLayoutOneColumn;
                // Add other flags as needed

                // Apply the new viewer preferences
                editor.ChangeViewerPreference(newPrefs);

                // Save the modified PDF using the facade's Save method
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"Viewer preferences updated and saved to '{outputPdf}'.");
    }
}