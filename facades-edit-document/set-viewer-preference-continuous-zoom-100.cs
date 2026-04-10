using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfContentEditor, PdfPageEditor, ViewerPreference

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

        // Change viewer preference to continuous layout (one column)
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);
            editor.ChangeViewerPreference(ViewerPreference.PageLayoutOneColumn);
            editor.Save(outputPath);
        }

        // Ensure default zoom (100%) – set zoom coefficient to 1.0
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(outputPath);
            pageEditor.Zoom = 1.0f; // 100%
            pageEditor.Save(outputPath);
        }

        Console.WriteLine($"Viewer preferences updated and saved to '{outputPath}'.");
    }
}