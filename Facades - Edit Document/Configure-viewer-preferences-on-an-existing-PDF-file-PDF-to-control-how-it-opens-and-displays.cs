using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        // Bind the existing PDF to the content editor facade
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Combine desired viewer preference flags (e.g., hide menu bar and use no page mode)
            int preferences = ViewerPreference.HideMenubar | ViewerPreference.PageModeUseNone;
            editor.ChangeViewerPreference(preferences);

            // Optional: read back the current preference flags
            int current = editor.GetViewerPreference();
            Console.WriteLine($"Current viewer preference flags: {current}");

            // Save the modified PDF to a new file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Viewer preferences applied and saved to '{outputPath}'.");
    }
}