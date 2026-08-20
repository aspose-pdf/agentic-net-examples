using System;
using Aspose.Pdf.Facades;

class PdfFullScreenToggle
{
    /// <summary>
    /// Toggles the FullScreen viewer preference of a PDF file.
    /// If the PDF is currently set to FullScreen, the flag is removed;
    /// otherwise the FullScreen flag is added.
    /// </summary>
    /// <param name="inputPath">Path to the source PDF.</param>
    /// <param name="outputPath">Path where the modified PDF will be saved.</param>
    public static void ToggleFullScreen(string inputPath, string outputPath)
    {
        // Ensure the source file exists.
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the PdfContentEditor facade.
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the PDF document to the editor.
        editor.BindPdf(inputPath);

        // Retrieve the current viewer preference flags.
        int currentPref = editor.GetViewerPreference();

        // Determine whether FullScreen mode is currently set.
        bool isFullScreen = (currentPref & ViewerPreference.PageModeFullScreen) != 0;

        // Toggle the FullScreen flag.
        int newPref;
        if (isFullScreen)
        {
            // Remove the FullScreen flag.
            newPref = currentPref & ~ViewerPreference.PageModeFullScreen;
        }
        else
        {
            // Add the FullScreen flag.
            newPref = currentPref | ViewerPreference.PageModeFullScreen;
        }

        // Apply the updated viewer preference.
        editor.ChangeViewerPreference(newPref);

        // Save the modified PDF to the specified output path.
        editor.Save(outputPath);

        // Optional: release resources (PdfContentEditor inherits from SaveableFacade which implements IDisposable).
        editor.Close();
    }

    // Example usage.
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string outputPdf = "sample_toggled.pdf";

        ToggleFullScreen(inputPdf, outputPdf);

        Console.WriteLine($"FullScreen preference toggled. Output saved to '{outputPdf}'.");
    }
}