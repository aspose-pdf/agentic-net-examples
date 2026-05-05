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

        // Combine desired viewer preferences using bitwise OR
        int viewerPrefs = ViewerPreference.CenterWindow | ViewerPreference.HideToolbar;

        // Edit the PDF with PdfContentEditor (facade)
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);                 // load the PDF
            editor.ChangeViewerPreference(viewerPrefs); // apply combined preferences
            editor.Save(outputPath);                   // save the modified PDF
        }

        Console.WriteLine($"PDF saved with combined viewer preferences to '{outputPath}'.");
    }
}