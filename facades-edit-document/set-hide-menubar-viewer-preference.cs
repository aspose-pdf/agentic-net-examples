using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfContentEditor, ViewerPreference

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

        // Create the facade, bind the PDF, modify viewer preferences, and save.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF file into the editor.
            editor.BindPdf(inputPath);

            // Retrieve current viewer preference flags.
            int currentPrefs = editor.GetViewerPreference();

            // Set the HideMenubar flag (bitwise OR with existing flags).
            int newPrefs = currentPrefs | ViewerPreference.HideMenubar;

            // Apply the updated viewer preferences.
            editor.ChangeViewerPreference(newPrefs);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Viewer preference updated. Output saved to '{outputPath}'.");
    }
}