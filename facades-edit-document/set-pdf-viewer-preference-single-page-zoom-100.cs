using System;
using System.IO;
using Aspose.Pdf;               // ViewerPreference
using Aspose.Pdf.Facades;      // PdfContentEditor, PdfPageEditor

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

        // -----------------------------------------------------------------
        // 1. Set viewer layout to single page (continuous layout)
        // -----------------------------------------------------------------
        PdfContentEditor contentEditor = new PdfContentEditor();
        contentEditor.BindPdf(inputPath);
        // ViewerPreference.PageLayoutSinglePage displays one page at a time
        contentEditor.ChangeViewerPreference(ViewerPreference.PageLayoutSinglePage);
        // Save the changes (overwrites or creates the output file)
        contentEditor.Save(outputPath);
        contentEditor.Close();

        // -----------------------------------------------------------------
        // 2. Ensure default zoom is 100% (zoom factor = 1.0)
        // -----------------------------------------------------------------
        PdfPageEditor pageEditor = new PdfPageEditor();
        pageEditor.BindPdf(outputPath);
        pageEditor.Zoom = 1.0f; // 100% zoom
        pageEditor.ApplyChanges();
        pageEditor.Save(outputPath);
        pageEditor.Close();

        Console.WriteLine($"Viewer preferences applied and saved to '{outputPath}'.");
    }
}