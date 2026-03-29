using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string tempPath = "temp.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Set a fixed zoom level for all pages (100%)
        PdfPageEditor pageEditor = new PdfPageEditor();
        pageEditor.BindPdf(inputPath);
        pageEditor.Zoom = 1.0f; // fixed zoom
        pageEditor.Save(tempPath);

        // Hide UI elements (including zoom controls) for consistent display
        PdfContentEditor contentEditor = new PdfContentEditor();
        contentEditor.BindPdf(tempPath);
        contentEditor.ChangeViewerPreference(ViewerPreference.HideWindowUI);
        contentEditor.Save(outputPath);

        // Clean up temporary file
        try
        {
            File.Delete(tempPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not delete temporary file: {ex.Message}");
        }

        Console.WriteLine($"PDF saved with fixed zoom and UI hidden: {outputPath}");
    }
}