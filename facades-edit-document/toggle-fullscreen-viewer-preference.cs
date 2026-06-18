using System;
using Aspose.Pdf.Facades;

public static class PdfViewerPreferenceHelper
{
    /// <summary>
    /// Toggles the Full‑Screen viewer preference of a PDF.
    /// </summary>
    /// <param name="inputPath">Path to the source PDF.</param>
    /// <param name="outputPath">Path where the modified PDF will be saved.</param>
    /// <param name="enableFullScreen">True to turn Full‑Screen on, false to turn it off.</param>
    public static void ToggleFullScreen(string inputPath, string outputPath, bool enableFullScreen)
    {
        // Ensure the source file exists before proceeding.
        if (!System.IO.File.Exists(inputPath))
            throw new System.IO.FileNotFoundException($"Input file not found: {inputPath}");

        // PdfContentEditor implements IDisposable via SaveableFacade, so wrap it in a using block.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPath);

            // Retrieve the current viewer preference flags.
            int currentPref = editor.GetViewerPreference();

            // Cast the enum value to int before using it in bitwise operations (per Aspose.Pdf requirements).
            int fullScreenFlag = (int)ViewerPreference.PageModeFullScreen;

            // Determine if the Full‑Screen flag is already set.
            bool isFullScreenSet = (currentPref & fullScreenFlag) != 0;

            // Adjust the flag based on the requested state.
            if (enableFullScreen && !isFullScreenSet)
            {
                // Turn Full‑Screen on.
                currentPref |= fullScreenFlag;
            }
            else if (!enableFullScreen && isFullScreenSet)
            {
                // Turn Full‑Screen off.
                currentPref &= ~fullScreenFlag;
            }

            // Apply the modified viewer preferences.
            editor.ChangeViewerPreference(currentPref);

            // Save the updated PDF to the specified output path.
            editor.Save(outputPath);
        }
    }
}

public class Program
{
    // Simple entry point required for a console‑type project.
    public static void Main(string[] args)
    {
        // Expected arguments: <inputPdf> <outputPdf> <enableFullScreen:true|false>
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: <inputPdf> <outputPdf> <enableFullScreen:true|false>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        bool enableFullScreen = bool.Parse(args[2]);

        PdfViewerPreferenceHelper.ToggleFullScreen(inputPath, outputPath, enableFullScreen);
        Console.WriteLine($"FullScreen viewer preference set to {enableFullScreen} for '{outputPath}'.");
    }
}