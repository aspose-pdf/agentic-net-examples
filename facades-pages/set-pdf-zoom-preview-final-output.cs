using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath   = "input.pdf";
        const string previewPath = "preview.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ---------- Preview: set zoom to 0.5 ----------
        // PdfPageEditor edits page content; the Zoom property controls the scaling factor.
        // 0.5f corresponds to 50% zoom (preview size).
        PdfPageEditor previewEditor = new PdfPageEditor();
        previewEditor.BindPdf(inputPath);
        previewEditor.Zoom = 0.5f;               // 50 % zoom for preview
        previewEditor.Save(previewPath);         // save the preview PDF
        previewEditor.Close();                   // release resources

        // ---------- Final output: set scale factor to 1.0 ----------
        // PdfViewer is used for viewing/printing. ScaleFactor = 1.0 means no scaling.
        PdfViewer finalViewer = new PdfViewer();
        finalViewer.BindPdf(previewPath);
        finalViewer.ScaleFactor = 1.0f;          // 100 % scale for final output
        // Example: print the document (optional). Comment out if not needed.
        // finalViewer.PrintDocument();
        finalViewer.Close();                     // release resources

        Console.WriteLine($"Preview saved to '{previewPath}' with zoom 0.5.");
        Console.WriteLine("Final viewer configured with scale factor 1.0.");
    }
}