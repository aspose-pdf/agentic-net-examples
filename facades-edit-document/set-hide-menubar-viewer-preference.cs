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

        // Use PdfContentEditor (facade) to modify viewer preferences
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPath);

            // Retrieve current viewer preference flags
            int currentPrefs = editor.GetViewerPreference();

            // Ensure the HideMenubar flag is set
            int newPrefs = currentPrefs | ViewerPreference.HideMenubar;

            // Apply the updated viewer preferences
            editor.ChangeViewerPreference(newPrefs);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Viewer preferences updated. Output saved to '{outputPath}'.");
    }
}