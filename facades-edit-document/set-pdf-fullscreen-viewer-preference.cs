using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_fullscreen.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the facade for content editing
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the source PDF file
        editor.BindPdf(inputPath);

        // Activate full‑screen viewer mode
        editor.ChangeViewerPreference(ViewerPreference.PageModeFullScreen);

        // Save the modified PDF
        editor.Save(outputPath);

        Console.WriteLine($"PDF saved with full‑screen preference: '{outputPath}'.");
    }
}