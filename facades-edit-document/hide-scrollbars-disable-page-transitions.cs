using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        // 1. Hide UI elements (scrollbars, toolbars, etc.) using ViewerPreference.HideWindowUI
        using (PdfContentEditor contentEditor = new PdfContentEditor())
        {
            contentEditor.BindPdf(inputPath);
            contentEditor.ChangeViewerPreference(ViewerPreference.HideWindowUI);
            // Save the intermediate result
            contentEditor.Save(outputPath);
        }

        // 2. Disable page transition effects by setting TransitionDuration to 0 for all pages
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(outputPath);
            pageEditor.TransitionDuration = 0; // No transition effect
            pageEditor.ApplyChanges();
            pageEditor.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}