using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "fullScreen_output.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the facade, bind the PDF, set full‑screen viewer preference, and save
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);                                   // Load the PDF
        editor.ChangeViewerPreference(ViewerPreference.PageModeFullScreen); // Enable full‑screen mode
        editor.Save(outputPath);                                      // Persist changes

        Console.WriteLine($"PDF saved with full‑screen preference to '{outputPath}'.");
    }
}