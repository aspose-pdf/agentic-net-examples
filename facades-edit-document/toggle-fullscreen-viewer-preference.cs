using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // PdfContentEditor, ViewerPreference

class PdfFullScreenToggler
{
    /// <summary>
    /// Toggles the FullScreen viewer preference of a PDF.
    /// </summary>
    /// <param name="inputPdf">Path to the source PDF.</param>
    /// <param name="outputPdf">Path where the modified PDF will be saved.</param>
    /// <param name="enableFullScreen">If true, enable FullScreen mode; otherwise disable it.</param>
    public static void ToggleFullScreen(string inputPdf, string outputPdf, bool enableFullScreen)
    {
        // Ensure the source file exists
        if (!File.Exists(inputPdf))
            throw new FileNotFoundException($"Input file not found: {inputPdf}");

        // PdfContentEditor implements IDisposable, so use a using block for deterministic disposal
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdf);

            // Retrieve the current viewer preference flags
            int currentPref = editor.GetViewerPreference();

            // Compute the new preference value by setting or clearing the FullScreen flag
            int newPref;
            if (enableFullScreen)
            {
                // Turn on FullScreen by adding the flag (bitwise OR)
                newPref = currentPref | ViewerPreference.PageModeFullScreen;
            }
            else
            {
                // Turn off FullScreen by removing the flag (bitwise AND with complement)
                newPref = currentPref & ~ViewerPreference.PageModeFullScreen;
            }

            // Apply the updated viewer preference
            editor.ChangeViewerPreference(newPref);

            // Save the modified PDF to the specified output path
            editor.Save(outputPdf);
        }
    }

    // Example usage
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPathOn = "sample_fullscreen_on.pdf";
        const string outputPathOff = "sample_fullscreen_off.pdf";

        // -------------------------------------------------------------------
        // Create a minimal PDF inline so the sandbox has a file to work with.
        // -------------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            using (Document seed = new Document())
            {
                seed.Pages.Add(); // add a blank page
                seed.Save(inputPath);
            }
        }

        // Enable FullScreen mode
        ToggleFullScreen(inputPath, outputPathOn, true);
        Console.WriteLine($"FullScreen enabled: {outputPathOn}");

        // Disable FullScreen mode
        ToggleFullScreen(inputPath, outputPathOff, false);
        Console.WriteLine($"FullScreen disabled: {outputPathOff}");
    }
}
