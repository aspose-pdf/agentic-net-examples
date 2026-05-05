using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfViewerPreferenceToggle
{
    /// <summary>
    /// Toggles the FullScreen viewer preference of a PDF.
    /// </summary>
    /// <param name="inputPath">Path to the source PDF file.</param>
    /// <param name="outputPath">Path where the modified PDF will be saved.</param>
    /// <param name="enableFullScreen">If true, enable FullScreen mode; otherwise disable it.</param>
    public static void ToggleFullScreen(string inputPath, string outputPath, bool enableFullScreen)
    {
        // Validate input file
        if (!File.Exists(inputPath))
            throw new FileNotFoundException($"Input PDF not found: {inputPath}");

        // Initialize the facade and bind the PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Retrieve current viewer preferences
        int currentPref = editor.GetViewerPreference();

        // Cast the enum value to int before using bitwise operators (Fix: cast-viewerpreference-to-int)
        int fullScreenFlag = (int)ViewerPreference.PageModeFullScreen;

        // Modify the PageModeFullScreen flag according to the desired state
        if (enableFullScreen)
            currentPref |= fullScreenFlag;   // set flag
        else
            currentPref &= ~fullScreenFlag;  // clear flag

        // Apply the updated preferences
        editor.ChangeViewerPreference(currentPref);

        // Save the result PDF
        editor.Save(outputPath);
    }

    // Entry point required for a console application (Fix: add Main method)
    public static void Main(string[] args)
    {
        // Expected arguments: <inputPath> <outputPath> <enableFullScreen:true|false>
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <inputPath> <outputPath> <enableFullScreen:true|false>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        bool enableFullScreen;
        if (!bool.TryParse(args[2], out enableFullScreen))
        {
            Console.WriteLine("The third argument must be 'true' or 'false'.");
            return;
        }

        try
        {
            ToggleFullScreen(inputPath, outputPath, enableFullScreen);
            Console.WriteLine($"FullScreen mode {(enableFullScreen ? "enabled" : "disabled")} and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

// Example usage from code (uncomment to test programmatically):
// PdfViewerPreferenceToggle.ToggleFullScreen("input.pdf", "output.pdf", true);   // enable FullScreen
// PdfViewerPreferenceToggle.ToggleFullScreen("input.pdf", "output.pdf", false);  // disable FullScreen