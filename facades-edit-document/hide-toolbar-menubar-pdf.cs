using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "minimal_ui.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using the PdfContentEditor facade
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Combine the HideToolbar and HideMenubar flags
        int viewerPrefs = ViewerPreference.HideToolbar | ViewerPreference.HideMenubar;
        editor.ChangeViewerPreference(viewerPrefs);

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"PDF saved with hidden toolbar and menubar: '{outputPath}'.");
    }
}