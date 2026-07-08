using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath      = "sample.pdf";
        const string previewPath    = "preview_zoom_0_5.pdf";
        const string finalOutputPath = "final_zoom_1_0.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ---------- Preview: set scale factor to 0.5 (50%) ----------
        // PdfViewer is a Facades class that supports ScaleFactor for preview purposes.
        using (PdfViewer viewer = new PdfViewer())
        {
            viewer.BindPdf(inputPath);          // load the PDF
            viewer.ScaleFactor = 0.5f;          // 50 % zoom for preview
            viewer.Save(previewPath);           // save the preview PDF
        }

        // ---------- Final output: set zoom to 1.0 (100%) ----------
        // PdfPageEditor allows editing page properties such as Zoom.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);          // load the same source PDF
            editor.Zoom = 1.0f;                 // 100 % zoom for final output
            editor.ApplyChanges();              // apply the zoom change
            editor.Save(finalOutputPath);       // save the final PDF
        }

        Console.WriteLine($"Preview saved to '{previewPath}' (zoom 0.5).");
        Console.WriteLine($"Final output saved to '{finalOutputPath}' (zoom 1.0).");
    }
}