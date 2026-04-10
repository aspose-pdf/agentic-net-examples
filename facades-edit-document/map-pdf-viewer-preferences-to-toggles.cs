using System;
using Aspose.Pdf.Facades;

class ViewerPreferenceMapper
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF with PdfContentEditor (facade) inside a using block for proper disposal
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPdf);

            // Retrieve current viewer preferences as a bitmask
            int currentPrefs = editor.GetViewerPreference();

            // Helper to display and toggle a specific flag
            bool PromptToggle(string description, int flag)
            {
                bool isSet = (currentPrefs & flag) != 0;
                Console.Write($"{description} (currently {(isSet ? "ON" : "OFF")}) - toggle? (y/n): ");
                string response = Console.ReadLine()?.Trim().ToLowerInvariant();
                return response == "y" || response == "yes";
            }

            // Build a new preferences bitmask based on user input
            int newPrefs = 0;

            // Example toggles – add or remove as needed
            if (PromptToggle("Hide Menu Bar", ViewerPreference.HideMenubar))
                newPrefs |= ViewerPreference.HideMenubar;

            if (PromptToggle("Hide Toolbar", ViewerPreference.HideToolbar))
                newPrefs |= ViewerPreference.HideToolbar;

            if (PromptToggle("Hide Window UI", ViewerPreference.HideWindowUI))
                newPrefs |= ViewerPreference.HideWindowUI;

            if (PromptToggle("Fit Window to First Page", ViewerPreference.FitWindow))
                newPrefs |= ViewerPreference.FitWindow;

            if (PromptToggle("Center Window on Screen", ViewerPreference.CenterWindow))
                newPrefs |= ViewerPreference.CenterWindow;

            if (PromptToggle("Display Document Title in Window", ViewerPreference.DisplayDocTitle))
                newPrefs |= ViewerPreference.DisplayDocTitle;

            if (PromptToggle("Full Screen Page Mode", ViewerPreference.PageModeFullScreen))
                newPrefs |= ViewerPreference.PageModeFullScreen;

            if (PromptToggle("Show Document Outline (Bookmarks)", ViewerPreference.PageModeUseOutlines))
                newPrefs |= ViewerPreference.PageModeUseOutlines;

            if (PromptToggle("Show Thumbnails", ViewerPreference.PageModeUseThumbs))
                newPrefs |= ViewerPreference.PageModeUseThumbs;

            // Apply the new viewer preferences
            editor.ChangeViewerPreference(newPrefs);

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Viewer preferences updated and saved to '{outputPdf}'.");
    }
}