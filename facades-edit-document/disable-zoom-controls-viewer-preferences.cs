using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_fixed_zoom.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF into the facade
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Disable zoom controls by hiding the toolbar (which contains zoom UI)
        // and hide other window UI elements for a clean, fixed‑zoom view.
        int viewerPrefs = ViewerPreference.HideToolbar | ViewerPreference.HideWindowUI;
        editor.ChangeViewerPreference(viewerPrefs);

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"PDF saved with fixed zoom preferences: {outputPath}");
    }
}