using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class FullScreenToggle
{
    public static void ToggleFullScreen(string inputPdf, string outputPdf, bool enableFullScreen)
    {
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);

            int currentPreference = editor.GetViewerPreference();
            int fullScreenFlag = (int)ViewerPreference.PageModeFullScreen;
            int newPreference;

            if (enableFullScreen)
                newPreference = currentPreference | fullScreenFlag; // enable
            else
                newPreference = currentPreference & ~fullScreenFlag; // disable

            editor.ChangeViewerPreference(newPreference);
            editor.Save(outputPdf);
        }

        Console.WriteLine($"FullScreen viewer preference {(enableFullScreen ? "enabled" : "disabled")} saved to {outputPdf}");
    }

    public static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "sample_toggled.pdf";

        // Example: enable FullScreen mode
        ToggleFullScreen(inputPath, outputPath, true);
    }
}