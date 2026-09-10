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

        // Create the facade object
        PdfContentEditor editor = new PdfContentEditor();

        // Load the PDF document
        editor.BindPdf(inputPath);

        // Hide the toolbar and the menu bar using ViewerPreference flags
        editor.ChangeViewerPreference(ViewerPreference.HideToolbar);
        editor.ChangeViewerPreference(ViewerPreference.HideMenubar);

        // Save the updated PDF
        editor.Save(outputPath);

        Console.WriteLine($"Viewer preferences applied and saved to '{outputPath}'.");
    }
}