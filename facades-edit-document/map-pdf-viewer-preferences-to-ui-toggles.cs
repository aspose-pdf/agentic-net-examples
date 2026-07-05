using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;                     // Core PDF types
using Aspose.Pdf.Facades;            // PdfContentEditor and ViewerPreference

class ViewerPreferenceMapper
{
    // Map each ViewerPreference flag to a readable name
    private static readonly Dictionary<string, int> PreferenceMap = new Dictionary<string, int>
    {
        { "HideMenubar",               ViewerPreference.HideMenubar },
        { "HideToolbar",               ViewerPreference.HideToolbar },
        { "HideWindowUI",              ViewerPreference.HideWindowUI },
        { "FitWindow",                 ViewerPreference.FitWindow },
        { "CenterWindow",              ViewerPreference.CenterWindow },
        { "DisplayDocTitle",           ViewerPreference.DisplayDocTitle },
        { "PageModeUseOutlines",       ViewerPreference.PageModeUseOutlines },
        { "PageModeUseThumbs",         ViewerPreference.PageModeUseThumbs },
        { "PageModeUseOC",             ViewerPreference.PageModeUseOC },
        { "PageModeUseNone",           ViewerPreference.PageModeUseNone },
        { "PageLayoutSinglePage",      ViewerPreference.PageLayoutSinglePage },
        { "PageLayoutOneColumn",       ViewerPreference.PageLayoutOneColumn },
        { "PageLayoutTwoColumnLeft",   ViewerPreference.PageLayoutTwoColumnLeft },
        { "PageLayoutTwoColumnRight",  ViewerPreference.PageLayoutTwoColumnRight }
        // Add other flags as needed
    };

    // Extract current toggle states from the bitmask
    private static Dictionary<string, bool> GetToggleStates(int prefValue)
    {
        var result = new Dictionary<string, bool>();
        foreach (var kvp in PreferenceMap)
        {
            result[kvp.Key] = (prefValue & kvp.Value) != 0;
        }
        return result;
    }

    // Build a new bitmask from the desired toggle states
    private static int BuildPreferenceValue(Dictionary<string, bool> toggles)
    {
        int value = 0;
        foreach (var kvp in toggles)
        {
            if (kvp.Value && PreferenceMap.TryGetValue(kvp.Key, out int flag))
                value |= flag;
        }
        return value;
    }

    // Example: simulate UI toggles (in a real UI these would come from checkboxes, etc.)
    private static Dictionary<string, bool> SimulateUserChanges(Dictionary<string, bool> currentToggles)
    {
        // Flip a couple of settings for demonstration
        if (currentToggles.ContainsKey("HideMenubar"))
            currentToggles["HideMenubar"] = !currentToggles["HideMenubar"];

        if (currentToggles.ContainsKey("FitWindow"))
            currentToggles["FitWindow"] = !currentToggles["FitWindow"];

        // Return the modified dictionary
        return currentToggles;
    }

    static void Main()
    {
        const string inputPath  = "sample.pdf";
        const string outputPath = "sample_modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Bind the document to PdfContentEditor
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(doc);

                // Retrieve current viewer preferences
                int currentPref = editor.GetViewerPreference();

                // Map to UI-friendly toggle dictionary
                var toggles = GetToggleStates(currentPref);

                // Display current states (simulating UI)
                Console.WriteLine("Current Viewer Preference Toggles:");
                foreach (var kvp in toggles)
                    Console.WriteLine($"{kvp.Key}: {(kvp.Value ? "ON" : "OFF")}");

                // Simulate user changing some toggles
                toggles = SimulateUserChanges(toggles);

                // Show updated states
                Console.WriteLine("\nUpdated Viewer Preference Toggles:");
                foreach (var kvp in toggles)
                    Console.WriteLine($"{kvp.Key}: {(kvp.Value ? "ON" : "OFF")}");

                // Build the new preference bitmask
                int newPref = BuildPreferenceValue(toggles);

                // Apply the new viewer preference
                editor.ChangeViewerPreference(newPref);

                // Save the modified PDF
                editor.Save(outputPath);
                Console.WriteLine($"\nModified PDF saved to '{outputPath}'.");
            }
        }
    }
}