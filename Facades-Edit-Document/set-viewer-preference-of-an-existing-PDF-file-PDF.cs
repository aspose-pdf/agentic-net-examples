using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Modify viewer preferences using PdfContentEditor
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the existing PDF
            editor.BindPdf(inputPath);

            // Combine desired flags (example: hide menubar and use no page mode)
            int viewerPrefs = ViewerPreference.HideMenubar | ViewerPreference.PageModeUseNone;
            editor.ChangeViewerPreference(viewerPrefs);

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Viewer preferences updated and saved to '{outputPath}'.");
    }
}