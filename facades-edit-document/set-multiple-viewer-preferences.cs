using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfContentEditor, ViewerPreference

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create the facade, bind the source PDF, set combined viewer preferences, and save.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // Combine preferences using bitwise OR.
        int combinedPrefs = ViewerPreference.CenterWindow | ViewerPreference.HideToolbar;
        editor.ChangeViewerPreference(combinedPrefs);

        // Save the modified PDF.
        editor.Save(outputPdf);

        Console.WriteLine($"Viewer preferences applied and saved to '{outputPdf}'.");
    }
}