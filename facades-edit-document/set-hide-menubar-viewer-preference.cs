using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfContentEditor, ViewerPreference

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor (facade) to edit viewer preferences
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPath);

            // Read current viewer preferences (bitmask of ViewerPreference flags)
            int currentPrefs = editor.GetViewerPreference();

            // Ensure the HideMenubar flag is set (preserve other flags)
            int newPrefs = currentPrefs | ViewerPreference.HideMenubar;

            // Apply the modified preferences
            editor.ChangeViewerPreference(newPrefs);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Viewer preference updated. Output saved to '{outputPath}'.");
    }
}