using System;
using System.IO;
using Aspose.Pdf;
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

        // Initialize the content editor facade
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPath);

            // Read current viewer preferences
            int currentPrefs = editor.GetViewerPreference();

            // Ensure the HideMenubar flag is set
            int newPrefs = currentPrefs | ViewerPreference.HideMenubar;

            // Apply the updated viewer preferences
            editor.ChangeViewerPreference(newPrefs);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Viewer preference updated and saved to '{outputPath}'.");
    }
}