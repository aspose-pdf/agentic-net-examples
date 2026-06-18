using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_fixed_zoom.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor implements IDisposable via SaveableFacade, so use using.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Combine viewer preference flags to hide the toolbar (which contains zoom controls)
            // and hide the window UI for a fixed, clean display.
            int viewerPrefs = ViewerPreference.HideToolbar | ViewerPreference.HideWindowUI;
            editor.ChangeViewerPreference(viewerPrefs);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with disabled zoom controls: '{outputPath}'.");
    }
}