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

        // Bind the PDF, disable UI elements that provide zoom controls,
        // and save the result.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Hide the toolbar (contains zoom buttons) and hide the window UI
            // to ensure the viewer cannot change the zoom level.
            int flags = ViewerPreference.HideToolbar | ViewerPreference.HideWindowUI;
            editor.ChangeViewerPreference(flags);

            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with disabled zoom controls: {outputPath}");
    }
}