using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Toggles the FullScreen viewer preference.
    // If 'enable' is true, FullScreen is turned on; otherwise it is turned off.
    static void ToggleFullScreen(string inputPath, string outputPath, bool enable)
    {
        // Initialize the facade and bind the source PDF.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Retrieve the current viewer preference flags.
        int currentPref = editor.GetViewerPreference();

        // Modify the FullScreen flag using bitwise operations.
        int newPref = enable
            ? currentPref | ViewerPreference.PageModeFullScreen          // set flag
            : currentPref & ~ViewerPreference.PageModeFullScreen;        // clear flag

        // Apply the updated viewer preferences.
        editor.ChangeViewerPreference(newPref);

        // Save the modified PDF.
        editor.Save(outputPath);
    }

    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Example usage: enable FullScreen mode.
        ToggleFullScreen(inputPdf, outputPdf, true);
        Console.WriteLine($"FullScreen viewer preference applied. Saved to '{outputPdf}'.");
    }
}