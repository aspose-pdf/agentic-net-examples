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

        // Change viewer preference to single page layout
        PdfContentEditor viewerEditor = new PdfContentEditor();
        try
        {
            viewerEditor.BindPdf(inputPath);
            viewerEditor.ChangeViewerPreference(ViewerPreference.PageLayoutSinglePage);
            viewerEditor.Save(outputPath);
        }
        finally
        {
            viewerEditor.Close();
        }

        // Ensure default zoom (100%) by setting page zoom to 1.0
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(outputPath);
            pageEditor.Zoom = 1.0f; // 100%
            pageEditor.Save(outputPath);
        }

        Console.WriteLine($"Viewer preference set and saved to '{outputPath}'.");
    }
}