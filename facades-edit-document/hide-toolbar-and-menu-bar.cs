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

        // Create the facade and bind the source PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Combine viewer preference flags to hide toolbar and menubar
        int viewerPrefs = ViewerPreference.HideToolbar | ViewerPreference.HideMenubar;
        editor.ChangeViewerPreference(viewerPrefs);

        // Save the updated PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"PDF saved with hidden toolbar and menubar: {outputPath}");
    }
}