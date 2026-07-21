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

        // 1. Hide toolbar (which contains zoom controls) via viewer preferences
        PdfContentEditor contentEditor = new PdfContentEditor();
        contentEditor.BindPdf(inputPath);
        contentEditor.ChangeViewerPreference(ViewerPreference.HideToolbar);
        contentEditor.Save(outputPath);
        contentEditor.Close();

        // 2. Set a fixed zoom level (e.g., 100%) for consistent display
        PdfPageEditor pageEditor = new PdfPageEditor();
        pageEditor.BindPdf(outputPath);
        pageEditor.Zoom = 1.0f; // 1.0 = 100%
        pageEditor.Save(outputPath);
        pageEditor.Close();

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}