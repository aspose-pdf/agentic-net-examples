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

        // Initialize the PdfContentEditor facade
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Combine viewer preferences using bitwise OR
        int combinedPreferences = ViewerPreference.CenterWindow | ViewerPreference.HideToolbar;
        editor.ChangeViewerPreference(combinedPreferences);

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close(); // optional cleanup

        Console.WriteLine($"PDF saved with updated viewer preferences to '{outputPath}'.");
    }
}