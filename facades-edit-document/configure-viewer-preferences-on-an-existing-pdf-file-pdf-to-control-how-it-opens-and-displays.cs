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

        // Use PdfContentEditor (a Facade) to modify viewer preferences
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the existing PDF
            editor.BindPdf(inputPath);

            // Combine desired ViewerPreference flags (bitwise OR)
            int preferences = ViewerPreference.HideMenubar |
                              ViewerPreference.PageModeUseNone |
                              ViewerPreference.FitWindow;

            // Apply the viewer preferences
            editor.ChangeViewerPreference(preferences);

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Viewer preferences applied and saved to '{outputPath}'.");
    }
}