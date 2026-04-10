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

        // Disable UI elements that provide zoom controls
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);
            editor.ChangeViewerPreference(ViewerPreference.HideToolbar);      // hide toolbar (contains zoom)
            editor.ChangeViewerPreference(ViewerPreference.HideWindowUI);    // hide other UI elements
            editor.Save(outputPath);
        }

        // Set a fixed zoom level (100%)
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(outputPath);
            pageEditor.Zoom = 1.0f; // 1.0 = 100%
            pageEditor.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}