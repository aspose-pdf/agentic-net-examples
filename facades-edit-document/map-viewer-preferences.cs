using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "sample_out.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF document
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Retrieve current viewer preferences
        int currentPrefs = editor.GetViewerPreference();

        // Map preferences to UI toggle variables (console demo)
        bool hideMenubar = (currentPrefs & ViewerPreference.HideMenubar) != 0;
        bool hideToolbar = (currentPrefs & ViewerPreference.HideToolbar) != 0;
        bool hideWindowUI = (currentPrefs & ViewerPreference.HideWindowUI) != 0;
        bool fullScreenMode = (currentPrefs & ViewerPreference.PageModeFullScreen) != 0;

        Console.WriteLine("Current Viewer Preferences:");
        Console.WriteLine($"  Hide Menubar   : {hideMenubar}");
        Console.WriteLine($"  Hide Toolbar   : {hideToolbar}");
        Console.WriteLine($"  Hide Window UI : {hideWindowUI}");
        Console.WriteLine($"  Full Screen    : {fullScreenMode}");

        // Simulate user toggling the options via console input
        Console.WriteLine("\nEnter new values (y/n) for each option (press Enter to keep current):");
        hideMenubar = PromptToggle("Hide Menubar", hideMenubar);
        hideToolbar = PromptToggle("Hide Toolbar", hideToolbar);
        hideWindowUI = PromptToggle("Hide Window UI", hideWindowUI);
        fullScreenMode = PromptToggle("Full Screen Mode", fullScreenMode);

        // Build the new preference flag value
        int newPrefs = 0;
        if (hideMenubar) newPrefs |= ViewerPreference.HideMenubar;
        if (hideToolbar) newPrefs |= ViewerPreference.HideToolbar;
        if (hideWindowUI) newPrefs |= ViewerPreference.HideWindowUI;
        if (fullScreenMode) newPrefs |= ViewerPreference.PageModeFullScreen;

        // Apply the updated preferences and save the PDF
        editor.ChangeViewerPreference(newPrefs);
        editor.Save(outputPath);

        Console.WriteLine($"Updated PDF saved as '{outputPath}'.");
    }

    private static bool PromptToggle(string name, bool current)
    {
        Console.Write($"{name} (current {(current ? "y" : "n")}): ");
        string input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
        {
            return current;
        }
        return input.Trim().StartsWith("y", StringComparison.OrdinalIgnoreCase);
    }
}