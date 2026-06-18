using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfContentEditor does not implement IDisposable, so we manage its lifetime manually.
        PdfContentEditor editor = new PdfContentEditor();

        // Load the PDF document.
        editor.BindPdf(inputPath);

        // Hide UI elements (including scrollbars) for a cleaner small‑screen view.
        editor.ChangeViewerPreference(ViewerPreference.HideWindowUI);

        // Save the modified PDF.
        editor.Save(outputPath);

        // Release resources.
        editor.Close();

        Console.WriteLine($"Viewer preference applied. Output saved to '{outputPath}'.");
    }
}