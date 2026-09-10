using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ViewerPreferenceMapper
{
    // Map ViewerPreference flags to readable names
    private static void DisplayCurrentPreferences(int prefValue)
    {
        Console.WriteLine("Current Viewer Preferences:");
        Console.WriteLine($"Hide Menubar          : {( (prefValue & ViewerPreference.HideMenubar) != 0 )}");
        Console.WriteLine($"Hide Toolbar          : {( (prefValue & ViewerPreference.HideToolbar) != 0 )}");
        Console.WriteLine($"Hide Window UI        : {( (prefValue & ViewerPreference.HideWindowUI) != 0 )}");
        Console.WriteLine($"Fit Window            : {( (prefValue & ViewerPreference.FitWindow) != 0 )}");
        Console.WriteLine($"Center Window         : {( (prefValue & ViewerPreference.CenterWindow) != 0 )}");
        Console.WriteLine($"Display Document Title: {( (prefValue & ViewerPreference.DisplayDocTitle) != 0 )}");
        Console.WriteLine($"Page Mode FullScreen  : {( (prefValue & ViewerPreference.PageModeFullScreen) != 0 )}");
        Console.WriteLine($"Page Mode Use Outlines : {( (prefValue & ViewerPreference.PageModeUseOutlines) != 0 )}");
        Console.WriteLine($"Page Mode Use Thumbs   : {( (prefValue & ViewerPreference.PageModeUseThumbs) != 0 )}");
        Console.WriteLine($"Non‑FullScreen PageMode Outlines : {( (prefValue & ViewerPreference.NonFullScreenPageModeUseOutlines) != 0 )}");
        Console.WriteLine();
    }

    // Prompt user for a Y/N answer and return true for Y, false for N
    private static bool PromptToggle(string description, bool currentValue)
    {
        Console.Write($"{description} (currently {(currentValue ? "ON" : "OFF")}) – toggle? (y/n): ");
        string input = Console.ReadLine()?.Trim().ToLowerInvariant();
        return input == "y" || input == "yes";
    }

    static void Main()
    {
        const string inputPdf  = "sample.pdf";
        const string outputPdf = "sample_modified.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block (ensures disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Initialize PdfContentEditor with the loaded document
            using (PdfContentEditor editor = new PdfContentEditor(doc))
            {
                // Retrieve current viewer preference flags
                int currentPref = editor.GetViewerPreference();

                // Show current settings to the user
                DisplayCurrentPreferences(currentPref);

                // Prepare a variable to accumulate changed flags
                int newPref = currentPref;

                // Example toggles – you can extend this list as needed
                if (PromptToggle("Hide Menubar", (currentPref & ViewerPreference.HideMenubar) != 0))
                    newPref ^= ViewerPreference.HideMenubar; // toggle flag

                if (PromptToggle("Hide Toolbar", (currentPref & ViewerPreference.HideToolbar) != 0))
                    newPref ^= ViewerPreference.HideToolbar;

                if (PromptToggle("Hide Window UI", (currentPref & ViewerPreference.HideWindowUI) != 0))
                    newPref ^= ViewerPreference.HideWindowUI;

                if (PromptToggle("Fit Window", (currentPref & ViewerPreference.FitWindow) != 0))
                    newPref ^= ViewerPreference.FitWindow;

                if (PromptToggle("Center Window", (currentPref & ViewerPreference.CenterWindow) != 0))
                    newPref ^= ViewerPreference.CenterWindow;

                if (PromptToggle("Display Document Title", (currentPref & ViewerPreference.DisplayDocTitle) != 0))
                    newPref ^= ViewerPreference.DisplayDocTitle;

                if (PromptToggle("Full‑Screen Page Mode", (currentPref & ViewerPreference.PageModeFullScreen) != 0))
                    newPref ^= ViewerPreference.PageModeFullScreen;

                if (PromptToggle("Show Outlines (Page Mode)", (currentPref & ViewerPreference.PageModeUseOutlines) != 0))
                    newPref ^= ViewerPreference.PageModeUseOutlines;

                if (PromptToggle("Show Thumbnails (Page Mode)", (currentPref & ViewerPreference.PageModeUseThumbs) != 0))
                    newPref ^= ViewerPreference.PageModeUseThumbs;

                if (PromptToggle("Show Outlines (Non‑FullScreen)", (currentPref & ViewerPreference.NonFullScreenPageModeUseOutlines) != 0))
                    newPref ^= ViewerPreference.NonFullScreenPageModeUseOutlines;

                // Apply changed preferences if any flag differs
                if (newPref != currentPref)
                {
                    // Aspose.Pdf.Facades.ChangeViewerPreference expects a single flag.
                    // To apply a new set of flags, we clear existing ones and set the new combination.
                    // First, clear all known flags by resetting the viewer preference to 0.
                    editor.ChangeViewerPreference(0);
                    // Then set the combined flags.
                    editor.ChangeViewerPreference(newPref);
                }

                // Save the modified PDF
                editor.Save(outputPdf);
                Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
            }
        }
    }
}