using System;
using System.IO;
using Aspose.Pdf;
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

        // Change viewer preference to continuous layout (one column)
        PdfContentEditor viewerEditor = new PdfContentEditor();
        viewerEditor.BindPdf(inputPath);
        viewerEditor.ChangeViewerPreference(ViewerPreference.PageLayoutOneColumn);
        viewerEditor.Save(outputPath);
        viewerEditor.Close();

        // Ensure default zoom is 100% (zoom factor 1.0)
        PdfPageEditor zoomEditor = new PdfPageEditor();
        zoomEditor.BindPdf(outputPath);
        zoomEditor.Zoom = 1.0f; // 100%
        zoomEditor.Save(outputPath);
        zoomEditor.Close();

        Console.WriteLine($"Viewer preferences set and saved to '{outputPath}'.");
    }
}