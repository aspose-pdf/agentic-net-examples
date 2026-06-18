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

        // Initialize the facade and bind the source PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Hide the toolbar and the menu bar using ViewerPreference flags
        editor.ChangeViewerPreference(ViewerPreference.HideToolbar);
        editor.ChangeViewerPreference(ViewerPreference.HideMenubar);

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Viewer preferences applied and saved to '{outputPath}'.");
    }
}